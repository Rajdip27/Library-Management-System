using CatMS;
using LibraryManagementSystem.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static LibraryManagementSystem.Auth_IdentityModel.IdentityModel;

namespace LibraryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        public AccountController(
          SignInManager<User> signInManager,
          UserManager<User> userManager,
          IAuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authService = authService;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.Register(model);

            if (!result.Success)
            {
                result.Errors.ForEach(e => ModelState.AddModelError("", e));
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(result.UserId.ToString());
            if (user == null)
                return RedirectToAction("Index", "Home");

            // Add role
            await _userManager.AddToRoleAsync(user, model.AccountType);

            // Sign in user
            await _signInManager.SignInAsync(user, isPersistent: false);

            // Role based redirect
            if (await _userManager.IsInRoleAsync(user, "Administrator") ||
                await _userManager.IsInRoleAsync(user, "Seller"))
            {
                return RedirectToAction(
                    actionName: "Index",
                    controllerName: "Dashboard",
                    routeValues: new { area = "Admin" }
                );
            }

            if (await _userManager.IsInRoleAsync(user, "Buyer"))
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
                return LocalRedirect("/Dashboard/Index");
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

    }
}

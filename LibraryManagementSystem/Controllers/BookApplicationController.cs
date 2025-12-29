using LibraryManagementSystem.Helper;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryManagementSystem.Controllers;

public class BookApplicationController : Controller
{
    private readonly IBookApplicationRepository _bookApplicationRepository;
    private readonly IBookRepository _bookRepository;
    private readonly ISignInHelper _signInHelper;

    public BookApplicationController(IBookApplicationRepository bookApplicationRepository, IBookRepository bookRepository, ISignInHelper signInHelper)
    {
        _bookApplicationRepository = bookApplicationRepository;
        _bookRepository = bookRepository;
        _signInHelper = signInHelper;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _bookApplicationRepository.GetAllBookApplicationAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
      public async Task<IActionResult> CreateOrEdit(int id, CancellationToken cancellationToken)
    {
        ViewData["BookId"] = _bookRepository.Dropdown();
        if (id == 0)
        {

            return View(new BookApplication());
        }
        else
        {
            var data = await _bookApplicationRepository.GetBookApplicationByIdAsync(id, cancellationToken);
            if (data != null)
            {
             
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(BookApplication bookApplication, CancellationToken cancellationToken)
    {
        ViewData["BookId"] = _bookRepository.Dropdown();
        if (bookApplication.Id == 0)
        {
            ViewData["BookId"] = _bookRepository.Dropdown();
            bookApplication.StudentId = _signInHelper.UserId ?? 1;
            bookApplication.Status = "Pending";
            await _bookApplicationRepository.AddBookApplicationAsync(bookApplication, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            
            await _bookApplicationRepository.UpdateBookApplicationAsync(bookApplication, cancellationToken);
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
     {
          var data = await _bookApplicationRepository.GetBookApplicationByIdAsync(id, cancellationToken);
          if (data != null)
          {
                return View(data);
          }
          return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _bookApplicationRepository.DeleteBookApplicationAsync(id, cancellationToken);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Applybook(int id, CancellationToken cancellationToken)
    {
        BookApplication bookApplication = new BookApplication();
        ViewData["BookId"] = _bookRepository.Dropdown();
        if (id > 0)
        {
            var data = await _bookRepository.GetBookByIdAsync(id, cancellationToken);
            bookApplication.BookId = data.Id;
            bookApplication.StudentId = _signInHelper.UserId ??1;

        }
        return View(bookApplication);
    }
}

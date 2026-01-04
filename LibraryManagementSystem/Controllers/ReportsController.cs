using LibraryManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            ViewData["Title"] = "Reports";
            ViewData["ActivePage"] = "Reports";

            // Simple example report data (you can expand later)
            ViewBag.TotalBooks = await _context.Books.CountAsync(cancellationToken);
            ViewBag.TotalMembers = await _context.Users.CountAsync(cancellationToken);
            ViewBag.TotalApplications = await _context.bookApplications.CountAsync(cancellationToken);

            return View();
        }
    }
}

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

    public BookApplicationController(IBookApplicationRepository bookApplicationRepository, IBookRepository bookRepository)
    {
        _bookApplicationRepository = bookApplicationRepository;
        _bookRepository = bookRepository;
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
        if (bookApplication.Id == 0)
        {
            ViewData["BookId"] = _bookRepository.Dropdown();
            await _bookApplicationRepository.AddBookApplicationAsync(bookApplication, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            ViewData["BookId"] = _bookRepository.Dropdown();
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

}

using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers;

public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookCategoryRepository _bookCategoryRepository;


    public BookController(IBookRepository bookRepository, IBookCategoryRepository bookCategoryRepository)
    {
        _bookRepository = bookRepository;
        _bookCategoryRepository = bookCategoryRepository;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _bookRepository.GetAllBookAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
   public async Task<IActionResult> CreateOrEdit(int id, CancellationToken cancellationToken)
    {
        ViewData["categoryId"] = _bookCategoryRepository.Dropdown();
        if (id == 0)
        {
           
            return View(new Book());
        }
        else
        {
            var data = await _bookRepository.GetBookByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(Book book, CancellationToken cancellationToken)
    {
        if(book.Id == 0)
        {
            await _bookRepository.AddBookAsync(book, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _bookRepository.UpdateBookAsync(book, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var data = await _bookRepository.GetBookByIdAsync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _bookRepository.DeleteBooktAsync(id, cancellationToken);
        return RedirectToAction("Index");
    }
}

using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository;

public interface IBookCategoryRepository
{
    Task<IEnumerable<BookCategory>> GetAllBookCategoryAsync(CancellationToken cancellationToken);
    Task<BookCategory> GetBookCategoryByIdAsync(int id, CancellationToken cancellationToken);
    Task<BookCategory> AddBookCategoryAsync(BookCategory  bookCategory,CancellationToken cancellationToken);
    Task<BookCategory> UpdateBookCategoryAsync(BookCategory  bookCategory,CancellationToken cancellationToken);
    Task<BookCategory> DeleteBookCategoryAsync(int id, CancellationToken cancellationToken);
}

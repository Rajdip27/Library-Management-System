using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBookAsync(CancellationToken cancellationToken);
    Task<Book> GetBookByIdAsync(int id, CancellationToken cancellationToken);
    Task<Book> AddBookAsync(Book book, CancellationToken cancellationToken);
    Task<Book> UpdateBookAsync(Book book, CancellationToken cancellationToken);
    Task<Book> DeleteBooktAsync(int id, CancellationToken cancellationToken);

}

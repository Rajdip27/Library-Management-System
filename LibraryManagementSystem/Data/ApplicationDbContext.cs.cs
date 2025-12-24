using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Book> Books { get; set; } 
    public DbSet<BookApplication> bookApplications { get; set; } 
    public DbSet<BookCategory> BookCategories { get; set; }
}

using LibraryManagementSystem.Auth_IdentityModel;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;
using static LibraryManagementSystem.Auth_IdentityModel.IdentityModel;

namespace LibraryManagementSystem.Data;

public class ApplicationDbContext: IdentityDbContext<
    IdentityModel.User,
    IdentityModel.Role,
    long,
    IdentityModel.UserClaim,
    IdentityModel.UserRole,
    IdentityModel.UserLogin,
    IdentityModel.RoleClaim,
    IdentityModel.UserToken>
    
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Book> Books { get; set; } 
    public DbSet<BookApplication> bookApplications { get; set; } 
    public DbSet<BookCategory> BookCategories { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Identity keys
        modelBuilder.Entity<UserLogin>().HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId });
        modelBuilder.Entity<UserRole>().HasKey(e => new { e.UserId, e.RoleId });
        modelBuilder.Entity<UserToken>().HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        modelBuilder.Entity<UserClaim>().HasKey(e => e.Id);
        modelBuilder.Entity<RoleClaim>().HasKey(e => e.Id);

        // BookCategory -> Book relationship
        modelBuilder.Entity<Book>()
            .HasOne(b => b.bookCategory)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // BookApplication -> Book relationship
        modelBuilder.Entity<BookApplication>()
            .HasOne(ba => ba.Book)
            .WithMany()
            .HasForeignKey(ba => ba.BookId)
            .OnDelete(DeleteBehavior.Restrict);

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(warnings =>
        warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.UseLoggerFactory(new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() }));
    }

}

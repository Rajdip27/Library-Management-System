using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models;

public class BookApplication: BaseEntities.BaseEntity<int>
{ 
    public string StudentEmail { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public String Status { get; set; } = "Pending"; // Pending, Approved, Rejected
    public Decimal FineAmount { get; set; } = 0;
    public DateTime IssueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Book? Book { get; set; }
    public int BookId { get; set; }
}

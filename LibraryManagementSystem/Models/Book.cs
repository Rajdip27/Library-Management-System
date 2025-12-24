namespace LibraryManagementSystem.Models;

public class Book: BaseEntities.BaseEntity<int>
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
}

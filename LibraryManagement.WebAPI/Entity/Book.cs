using System;

namespace LibraryManagement.WebAPI.Entity;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public int YearPublished { get; set; }
    public bool IsAvailable { get; set; }
    public DateOnly? DueDate { get; set; }
    public int MemberId { get; set; }
}
// This class represents a book entity in the library management system.
// It contains properties for the book's ID, title, author, and year published.
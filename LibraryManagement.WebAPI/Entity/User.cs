using System;

namespace LibraryManagement.WebAPI.Entity;

public class UserModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string PasswordHash { get; set; } // Hashed password
    public string Role { get; set; } // e.g., "Admin", "User"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true; // Indicates if the user is active
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime? LastLogin { get; set; } // Tracks last login timestamp
    public List<string> Claims { get; set; } = new List<string>(); // Stores user claims



}

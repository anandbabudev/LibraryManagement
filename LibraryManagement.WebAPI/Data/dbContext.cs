using System;
using LibraryManagement.WebAPI.Entity; // Ensure this namespace contains the 'Member' class
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.WebAPI.Data;

public class dbContext : DbContext
{
    public dbContext(DbContextOptions<dbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;
    // public DbSet<Member> Members { get; set; } = null!;
    // public DbSet<Loan> Loans { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Book>().ToTable("Books");
        // modelBuilder.Entity<Member>().ToTable("Members");
        // modelBuilder.Entity<Loan>().ToTable("Loans");
    }
}
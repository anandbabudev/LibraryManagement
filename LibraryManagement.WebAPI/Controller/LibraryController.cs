using LibraryManagement.WebAPI.Data;
using LibraryManagement.WebAPI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController(dbContext context) : ControllerBase
    {
        public readonly dbContext _context = context;
        // GET: api/library
        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _context.Books.ToList();
            return Ok(books);
        }
        // GET: api/library/5   
        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        // POST: api/library
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null.");
            }
            _context.Books.Add(book);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }
        // PUT: api/library/5
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("Book ID mismatch.");
            }
            var existingBook = _context.Books.Find(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.YearPublished = book.YearPublished;
            _context.SaveChanges();
            return NoContent();
        }
        // DELETE: api/library/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return NoContent();
        }
        // GET: api/library/search?title=someTitle
        [HttpGet("search")]
        public IActionResult SearchBooks([FromQuery] string title)
        {
            var books = _context.Books.Where(b => b.Title != null && b.Title.Contains(title)).ToList() ?? new List<Book>();
            if (books.Count == 0)
            {
                return NotFound("No books found with the given title.");
            }
            return Ok(books);
        }
        // GET: api/library/author?name=someAuthor
        [HttpGet("author")]
        public IActionResult GetBooksByAuthor(string name)
        {
            var books = _context.Books?.Where(b => b.Author != null && b.Author.Contains(name)).ToList() ?? new List<Book>();
            if (books.Count == 0)
            {
                return NotFound("No books found by the given author.");
            }
            return Ok(books);
        }
        // GET: api/library/year?year=2023
        [HttpGet("year")]
        public IActionResult GetBooksByYear(int year)
        {
            var books = _context.Books.Where(b => b.YearPublished == year).ToList();
            if (books.Count == 0)
            {
                return NotFound("No books found published in the given year.");
            }
            return Ok(books);
        }
        // GET: api/library/available
        [HttpGet("available")]
        public IActionResult GetAvailableBooks()
        {
            var availableBooks = _context.Books.Where(b => b.IsAvailable).ToList();
            if (availableBooks.Count == 0)
            {
                return NotFound("No available books found.");
            }
            return Ok(availableBooks);
        }
        // GET: api/library/loaned
        [HttpGet("loaned")]
        public IActionResult GetLoanedBooks()
        {
            var loanedBooks = _context.Books.Where(b => !b.IsAvailable).ToList();
            if (loanedBooks.Count == 0)
            {
                return NotFound("No loaned books found.");
            }
            return Ok(loanedBooks);
        }
        // GET: api/library/overdue
        [HttpGet("overdue")]
        public IActionResult GetOverdueBooks()
        {
            // Assuming you have a method to check for overdue books
            var overdueBooks = _context.Books.Where(b => b.IsAvailable == false && b.DueDate <= DateOnly.FromDateTime(DateTime.Now)).ToList();
            if (overdueBooks.Count == 0)
            {
                return NotFound("No overdue books found.");
            }
            return Ok(overdueBooks);
        }
        // GET: api/library/loaned/{memberId}
        [HttpGet("loaned/{memberId}")]
        public IActionResult GetLoanedBooksByMember(int memberId)
        {
            var loanedBooks = _context.Books.Where(b => b.IsAvailable == false && b.MemberId == memberId).ToList();
            if (loanedBooks.Count == 0)
            {
                return NotFound("No loaned books found for the given member.");
            }
            return Ok(loanedBooks);
        }

    }
}

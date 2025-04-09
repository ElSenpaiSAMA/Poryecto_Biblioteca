using Biblioteca.Application;
using Biblioteca.Domain;
using Biblioteca.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(string? search = null)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(search) ||
                    b.Author.ToLower().Contains(search) ||
                    b.Categories.Any(c => c.ToLower().Contains(search)));
            }

            var books = await query
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Author = b.Author,
                    Year = b.Year,
                    ImageData = Convert.ToBase64String(b.ImageData),
                    Categories = b.Categories
                })
                .ToListAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Year = book.Year,
                ImageData = Convert.ToBase64String(book.ImageData),
                Categories = book.Categories
            };
            return Ok(bookDto);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook([FromForm] string title, [FromForm] string? description,
            [FromForm] string author, [FromForm] int year, [FromForm] IFormFile image, [FromForm] string categories)
        {
            if (image == null || image.Length == 0) return BadRequest("La imagen es requerida.");

            using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);

            var book = new Book
            {
                Title = title,
                Description = description,
                Author = author,
                Year = year,
                ImageData = memoryStream.ToArray(),
                Categories = categories.Split(',').Select(c => c.Trim()).ToList()
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Year = book.Year,
                ImageData = Convert.ToBase64String(book.ImageData),
                Categories = book.Categories
            };

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] string title, [FromForm] string? description,
            [FromForm] string author, [FromForm] int year, [FromForm] IFormFile? image, [FromForm] string categories)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            book.Title = title;
            book.Description = description;
            book.Author = author;
            book.Year = year;
            book.Categories = categories.Split(',').Select(c => c.Trim()).ToList();

            if (image != null && image.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                book.ImageData = memoryStream.ToArray();
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
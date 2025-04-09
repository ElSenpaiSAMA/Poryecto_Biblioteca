using Biblioteca.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IFavoriteBookService _favoriteBookService;

        public BooksController(IBookService bookService, IFavoriteBookService favoriteBookService)
        {
            _bookService = bookService;
            _favoriteBookService = favoriteBookService;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks(string? search = null)
        {
            var books = await _bookService.GetAllBooksAsync(search);
            var bookDtos = books.Select(b => new
            {
                b.Id,
                b.Title,
                b.Description,
                b.Author,
                b.Year,
                ImageData = Convert.ToBase64String(b.ImageData),
                b.Categories,
                IsFavorite = _favoriteBookService.GetFavoriteBookByBookIdAsync(b.Id).Result?.FavoriteState ?? false
            }).ToList();
            return Ok(bookDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            var favorite = await _favoriteBookService.GetFavoriteBookByBookIdAsync(id);
            var bookDto = new
            {
                book.Id,
                book.Title,
                book.Description,
                book.Author,
                book.Year,
                ImageData = Convert.ToBase64String(book.ImageData),
                book.Categories,
                IsFavorite = favorite?.FavoriteState ?? false
            };
            return Ok(bookDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook([FromForm] string title, [FromForm] string? description,
            [FromForm] string author, [FromForm] int year, [FromForm] IFormFile image, [FromForm] string categories)
        {
            if (image == null || image.Length == 0) return BadRequest("La imagen es requerida.");

            var book = await _bookService.CreateBookAsync(title, description, author, year, image, categories);
            var bookDto = new
            {
                book.Id,
                book.Title,
                book.Description,
                book.Author,
                book.Year,
                ImageData = Convert.ToBase64String(book.ImageData),
                book.Categories,
                IsFavorite = false
            };

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] string title, [FromForm] string? description,
            [FromForm] string author, [FromForm] int year, [FromForm] IFormFile? image, [FromForm] string categories)
        {
            try
            {
                await _bookService.UpdateBookAsync(id, title, description, author, year, image, categories);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/favorite")]
        public async Task<IActionResult> ToggleFavorite(int id)
        {
            try
            {
                await _favoriteBookService.ToggleFavoriteBookAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
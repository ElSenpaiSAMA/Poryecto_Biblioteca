using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync(string search = null);
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(string title, string? description, string author, int year, IFormFile image, string categories);
        Task UpdateBookAsync(int id, string title, string? description, string author, int year, IFormFile? image, string categories);
        Task DeleteBookAsync(int id);

    }
}

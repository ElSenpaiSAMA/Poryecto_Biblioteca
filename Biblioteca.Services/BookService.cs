using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using Biblioteca.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Biblioteca.Domain.Models;


namespace Biblioteca.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetAllBooksAsync(string search = null)
        {
            return await _bookRepository.GetAllAsync(search);
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> CreateBookAsync(string title, string? description, string author, int year, IFormFile image, string categories)
        {
            byte[] imageData;
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                imageData = memoryStream.ToArray();
            }

            var book = new Book
            {
                Title = title,
                Description = description,
                Author = author,
                Year = year,
                ImageData = imageData,
                Categories = categories.Split(',').Select(c => c.Trim()).ToList()
            };

            return await _bookRepository.AddAsync(book);
        }

        public async Task UpdateBookAsync(int id, string title, string? description, string author, int year, IFormFile? image, string categories)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new Exception("Book not found");

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

            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
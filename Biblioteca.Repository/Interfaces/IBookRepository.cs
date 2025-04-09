using Biblioteca.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(string search = null);
        Task<Book> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
    }
}
using Biblioteca.Domain.Models;
using System.Threading.Tasks;

namespace Biblioteca.Repository.Interfaces
{
    public interface IFavoriteBookRepository
    {
        Task<FavoriteBook> GetByBookIdAsync(int bookId);
        Task<FavoriteBook> AddAsync(FavoriteBook favoriteBook);
        Task UpdateAsync(FavoriteBook favoriteBook);
        Task DeleteAsync(int id);
    }
}
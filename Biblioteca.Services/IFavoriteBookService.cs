using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Domain.Models;

namespace Biblioteca.Services
{
    public interface IFavoriteBookService
    {
        Task<FavoriteBook> GetFavoriteBookByBookIdAsync(int bookId);
        Task<FavoriteBook> AddFavoriteBookAsync(int bookId);
        Task ToggleFavoriteBookAsync(int bookId);
        Task DeleteFavoriteBookAsync(int id);
    }
}

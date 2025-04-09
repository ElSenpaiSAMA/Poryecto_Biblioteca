using System.Threading.Tasks;
using Biblioteca.Repository.Interfaces;
using Biblioteca.Domain.Models;

namespace Biblioteca.Services
{
    public class FavoriteBookService : IFavoriteBookService
    {
        private readonly IFavoriteBookRepository _favoriteBookRepository;

        public FavoriteBookService(IFavoriteBookRepository favoriteBookRepository)
        {
            _favoriteBookRepository = favoriteBookRepository;
        }

        public async Task<FavoriteBook> GetFavoriteBookByBookIdAsync(int bookId)
        {
            return await _favoriteBookRepository.GetByBookIdAsync(bookId);
        }

        public async Task<FavoriteBook> AddFavoriteBookAsync(int bookId)
        {
            var favoriteBook = new FavoriteBook
            {
                BookId = bookId,
                FavoriteState = true
            };
            return await _favoriteBookRepository.AddAsync(favoriteBook);
        }

        public async Task ToggleFavoriteBookAsync(int bookId)
        {
            var favoriteBook = await _favoriteBookRepository.GetByBookIdAsync(bookId);
            if (favoriteBook == null)
            {
                await AddFavoriteBookAsync(bookId);
            }
            else
            {
                favoriteBook.FavoriteState = !favoriteBook.FavoriteState;
                await _favoriteBookRepository.UpdateAsync(favoriteBook);
            }
        }

        public async Task DeleteFavoriteBookAsync(int id)
        {
            await _favoriteBookRepository.DeleteAsync(id);
        }
    }
}
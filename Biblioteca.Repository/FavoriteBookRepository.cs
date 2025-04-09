using System.Threading.Tasks;
using Biblioteca.Repository.Interfaces;
using Biblioteca.Domain.Models;
using Biblioteca.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repository
{
    public class FavoriteBookRepository : IFavoriteBookRepository
    {
        private readonly LibraryDbContext _context;

        public FavoriteBookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<FavoriteBook> GetByBookIdAsync(int bookId)
        {
            return await _context.FavoriteBooks.FirstOrDefaultAsync(fb => fb.BookId == bookId);
        }

        public async Task<FavoriteBook> AddAsync(FavoriteBook favoriteBook)
        {
            _context.FavoriteBooks.Add(favoriteBook);
            await _context.SaveChangesAsync();
            return favoriteBook;
        }

        public async Task UpdateAsync(FavoriteBook favoriteBook)
        {
            _context.FavoriteBooks.Update(favoriteBook);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var favoriteBook = await _context.FavoriteBooks.FindAsync(id);
            if (favoriteBook != null)
            {
                _context.FavoriteBooks.Remove(favoriteBook);
                await _context.SaveChangesAsync();
            }
        }
    }
}
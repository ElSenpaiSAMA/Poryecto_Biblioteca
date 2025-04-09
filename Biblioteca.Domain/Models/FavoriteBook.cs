namespace Biblioteca.Domain.Models
{
    public class FavoriteBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public bool FavoriteState { get; set; }
    }
}
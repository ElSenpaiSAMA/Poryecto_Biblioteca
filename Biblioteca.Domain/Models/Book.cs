using System.Collections.Generic;

namespace Biblioteca.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public byte[] ImageData { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
    }
}
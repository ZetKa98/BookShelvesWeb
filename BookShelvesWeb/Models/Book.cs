using System.ComponentModel.DataAnnotations;

namespace BookShelvesWeb.Models
{
    public class Book : EntityBase
    {
        [Required]
        public String Name { get; set; }

        public Int32? Year { get; set; }
        
        public IList<BookGenre> BookGenres { get; set; }

        [Required]
        public Int32 AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
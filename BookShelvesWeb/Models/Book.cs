using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookShelvesWeb.Models
{
    public class Book : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public int? Year { get; set; }
        
        public IList<BookGenre> BookGenres { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}

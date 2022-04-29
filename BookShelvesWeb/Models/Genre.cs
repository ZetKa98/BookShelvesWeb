using System.ComponentModel.DataAnnotations;

namespace BookShelvesWeb.Models
{
    public class Genre : EntityBase
    {
        [Required]
        public String Name { get; set; }

        public IList<BookGenre> BookGenres { get; set; }
    }
}

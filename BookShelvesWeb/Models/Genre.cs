using System.ComponentModel.DataAnnotations;

namespace BookShelvesWeb.Models
{
    public class Genre : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public IList<BookGenre> BookGenres { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BookShelvesWeb.Models
{
    public class Author : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
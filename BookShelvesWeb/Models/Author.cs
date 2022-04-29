using System.ComponentModel.DataAnnotations;

namespace BookShelvesWeb.Models
{
    public class Author : EntityBase
    {
        [Required]
        public String Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
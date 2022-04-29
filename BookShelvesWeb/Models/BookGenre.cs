namespace BookShelvesWeb.Models
{
    public class BookGenre
    {
        public Int32 BookId { get; set; }
        public Book Book { get; set; }

        public Int32 GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
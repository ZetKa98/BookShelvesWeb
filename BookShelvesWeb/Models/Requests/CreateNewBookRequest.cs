using AutoMapper;
using BookShelvesWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace BookShelvesWeb
{
    public class CreateNewBookRequest
    {
        public Int32 Id { get; set; }

        [Required]
        public String Name { get; set; }    

        public Int32? Year { get; set; }

        [Required]
        public Int32 AuthorId { get; set; }

        [Required]
        public List<Int32> GenresId { get; set; }

        public Book GetBookEntityFromRequest()
        {
            var book = new Book();

            var config = new MapperConfiguration(cfg => 
                cfg.CreateMap<CreateNewBookRequest, Book>());

            var mapper = new Mapper(config);
            mapper.Map(this, book);

            book.BookGenres = new List<BookGenre>();
            this.GenresId.ForEach(id => book.BookGenres.Add(new BookGenre { GenreId = id}));

            return book;
        }
    }
}
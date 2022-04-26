using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BookShelvesWeb.Models
{
    public class BookShelvesRepository : IBookShelvesRepository
    {
        BookShelvesDbContext _context;

        public BookShelvesRepository(BookShelvesDbContext context)
        {
            _context = context;
        }

        public IQueryable<Book> GetAllBooks()
        {
            return _context.Books;
        }

        public async Task<Book> AddNewBook(Book book)
        {
            await _context.AddAsync(book);

            await _context.SaveChangesAsync();
            Console.WriteLine(book.Name);
            return book;
        }

        public async Task<Book> DeleteBookById(int id)
        {
            var foundedBook = await _context.Books.FirstAsync(book => book.Id == id);
            _context.Books.Remove(foundedBook);

            return foundedBook;
        }

        public async Task<Book> UpdateBook(Book bookForUpdate)
        {
            var foundedBook = await _context.Books.FirstAsync(book => book.Id == bookForUpdate.Id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Book, Book>());
            var mapper = new Mapper(config);
            mapper.Map(bookForUpdate, foundedBook);

            await _context.SaveChangesAsync();

            return foundedBook;
        }

        public async Task<Author> AddNewAuthor(Author author)
        {
            await _context.Authors.AddAsync(author);

            await _context.SaveChangesAsync();

            return author;
        }

        public async Task<Genre> AddNewGenre(Genre genre)
        {
            await _context.Genres.AddAsync(genre);

            await _context.SaveChangesAsync();

            return genre;
        }
    }
}

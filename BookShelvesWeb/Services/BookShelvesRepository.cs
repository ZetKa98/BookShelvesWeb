using AutoMapper;
using BookShelvesWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShelvesWeb.Services
{
    public class BookShelvesRepository : IBookShelvesRepository
    {
        BookShelvesDbContext _context;

        public BookShelvesRepository(BookShelvesDbContext context)
        {
            _context = context;
        }

        #region Public Methods
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            using (_context)
            {
                return await _context.Books?
                    .Include(book => book.Author)
                    .Include(book => book.BookGenres)
                    .ThenInclude(book => book.Genre)
                    .ToListAsync();
            }
       
        }

        public async Task<Book> AddNewBook(Book book)
        {
            using (_context)
            {
                await _context.AddAsync(book);

                await _context.SaveChangesAsync();

                return book;
            }
        }

        public async Task<Book> UpdateBook(Book bookForUpdate)
        {
            using (_context)
            {
                var foundedBook = await _context.Books.Include(x => x.BookGenres).FirstAsync(book => book.Id == bookForUpdate.Id);
                
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Book, Book>().ForMember(dest => dest.BookGenres, act => act.Ignore()));
                var mapper = new Mapper(config);
                mapper.Map(bookForUpdate, foundedBook);

                bookForUpdate.BookGenres
                    .ExceptBy(foundedBook.BookGenres.Select(x => x.GenreId), x => x.GenreId).ToList()
                    .ForEach(x => foundedBook.BookGenres.Add(new BookGenre {GenreId = x.GenreId }));

                await _context.SaveChangesAsync();

                return foundedBook;
            }
        }

        public async Task<Book> DeleteBookById(Int32 id)
        {
            using (_context) 
            {
                var foundedBook = await _context.Books.FirstAsync(book => book.Id == id);
                _context.Books.Remove(foundedBook);

                await _context.SaveChangesAsync();

                return foundedBook;
            }
        }

        public async Task<Author> AddNewAuthor(Author author)
        {
            using (_context)
            {
                await _context.Authors.AddAsync(author);

                await _context.SaveChangesAsync();

                return author;
            }
        }

        public async Task<Genre> AddNewGenre(Genre genre)
        {
            using (_context)
            {
                await _context.Genres.AddAsync(genre);

                await _context.SaveChangesAsync();

                return genre;
            }
        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            using (_context)
            {
                return await _context.Genres?.ToListAsync();
            }
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            using (_context)
            {
                return await _context.Authors?.ToListAsync();
            }
        }

        #endregion

    }
}
using BookShelvesWeb.Models;

namespace BookShelvesWeb.Services
{
    public interface IBookShelvesRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> AddNewBook(Book book);
        Task<Book> UpdateBook(Book bookForUpdate);
        Task<Book> DeleteBookById(Int32 id);

        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> AddNewAuthor(Author author);

        Task<IEnumerable<Genre>> GetGenres();
        Task<Genre> AddNewGenre(Genre genre);
    }
}
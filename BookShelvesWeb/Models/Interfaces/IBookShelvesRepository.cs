namespace BookShelvesWeb.Models
{
    public interface IBookShelvesRepository
    {
        IQueryable<Book> GetAllBooks();
        Task<Book> AddNewBook(Book book);
        Task<Author> AddNewAuthor(Author author);
        Task<Genre> AddNewGenre(Genre genre);
        Task<Book> UpdateBook(Book bookForUpdate);
        Task<Book> DeleteBookById(int id);
    }
}

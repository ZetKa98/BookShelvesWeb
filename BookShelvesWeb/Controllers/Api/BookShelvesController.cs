using BookShelvesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BookShelvesWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookShelvesController : ControllerBase
    {
        IBookShelvesRepository _repository;
        ILogger<BookShelvesController> _logger;

        public BookShelvesController(IBookShelvesRepository repository, ILogger<BookShelvesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                var books =  _repository.GetAllBooks();
                return Ok(books);
            }
            catch(Exception e) 
            {
                return GetBadRequest(e);
            }
        }

        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook(Book newBook)
        {
            try
            {
                var addedBook = await _repository.AddNewBook(newBook);

                return Ok();
            }
            catch (Exception e)
            {
                return GetBadRequest(e);
            }
        }

        [HttpPut("update-book")]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            try
            {
                await _repository.UpdateBook(book);
                return Ok();
            }
            catch(Exception e)
            {
                return GetBadRequest(e);
            }
        }

        [HttpDelete("delete-book/{id}")]
        public async Task<IActionResult> DeleteBookById([FromQuery]int id)
        {
            try
            {
                await _repository.DeleteBookById(id);
                return Ok();
            }
            catch (Exception e)
            {
                return GetBadRequest(e);
            }
        }

        [HttpPost("add-author")]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            try
            {
                await _repository.AddNewAuthor(author);

                return Ok();
            }
            catch (Exception e)
            {
                return GetBadRequest(e);
            }
        }

        [HttpPost("add-genre")]
        public async Task<IActionResult> AddGenre(Genre genre)
        {
            try
            {
                await _repository.AddNewGenre(genre);

                return Ok();
            }
            catch (Exception e)
            {
                return GetBadRequest(e);
            }
        }

        private IActionResult GetBadRequest(Exception exception)
        {
            SetErrorLogToConsole(exception.Message);
            return BadRequest();
        }

        private void SetErrorLogToConsole(string message, [CallerMemberName]string member = null)
        {
            _logger.LogError(message);
        }
    }
}
using BookShelvesWeb.Models;
using BookShelvesWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BookShelvesWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookShelvesController : ControllerBase
    {
        #region Fields

        IBookShelvesRepository _repository;
        ILogger<BookShelvesController> _logger;

        #endregion

        public BookShelvesController(IBookShelvesRepository repository, ILogger<BookShelvesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        #region Actions

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var books =  await _repository.GetAllBooks();
                return Ok(books);
            }
            catch(Exception e) 
            {
                return GetBadRequest(e);
            }
        }

        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromBody]CreateNewBookRequest newBookRequest)
        {
            var book = newBookRequest.GetBookEntityFromRequest();
            
            try
            {
                var addedBook = await _repository.AddNewBook(book);

                return Ok(addedBook);
            }
            catch (Exception e)
            {
                return GetBadRequest(e);
            }
        }

        [HttpPut("update-book")]
        public async Task<IActionResult> UpdateBook([FromBody]CreateNewBookRequest updateBookRequest)
        {
            var book = updateBookRequest.GetBookEntityFromRequest();
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

        [HttpDelete("delete-book")]
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

        [HttpGet("all-authors")]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                var authors = await _repository.GetAuthors();
                return Ok(authors);
            }
            catch (Exception e)
            {
                return GetBadRequest(e);
            }
        }

        [HttpPost("add-author")]
        public async Task<IActionResult> AddAuthor([FromBody]Author author)
        {
            try
            {
                var addedAuthor = await _repository.AddNewAuthor(author);

                return Ok(addedAuthor);
            }
            catch (Exception e)
            {
                return GetBadRequest(e);
            }
        }

        [HttpGet("all-genres")]
        public async Task<IActionResult> GetGenres()
        {
            try
            {
                var genres = await _repository.GetGenres();
                return Ok(genres);
            }
            catch (Exception e)
            {
                return GetBadRequest(e);
            }
        }

        [HttpPost("add-genre")]
        public async Task<IActionResult> AddGenre([FromBody] Genre genre)
        {
            try
            {
                var addedGenre = await _repository.AddNewGenre(genre);

                return Ok(addedGenre);
            }
            catch (Exception e)
            {
                return GetBadRequest(e);
            }
        }

        #endregion

        #region Private Methods

        private IActionResult GetBadRequest(Exception exception)
        {
            SetErrorLogToConsole(exception.Message);
            return BadRequest();
        }

        private void SetErrorLogToConsole(string message, [CallerMemberName] string member = null)
        {
            _logger.LogError(message);
        }

        #endregion 

    }
}
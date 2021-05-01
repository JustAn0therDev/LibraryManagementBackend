using Entities;
using LibraryManagementBackend.Requests;
using LibraryManagementBackend.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using UseCases.Interfaces;


namespace LibraryManagementBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookUseCase _useCase;
        private readonly ILogger _logger;

        public BookController(ILogger logger, IBookUseCase useCase)
        {
            _useCase = useCase;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _useCase.GetAll();
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateBook createBook)
        {
            try 
            {
                var bookName = createBook.Name;
                var authorId = createBook.AuthorID;
                var publisherId = createBook.PublisherID;
                var genreId = createBook.GenreID;

                var book = _useCase.MakeObject(bookName, authorId, publisherId, genreId);

                book = _useCase.Save(book);

                var author = book.Author;
                var publisher = book.Publisher;
                var genre = book.Genre;

                var createBookResponse = new CreateBookResponse 
                {
                    BookName = book.Name,
                    AuthorName = book.Author?.Name,
                    PublisherName = book.Publisher?.Name,
                    GenreName = book.Genre?.Name,
                };

                return Ok(createBookResponse);
            }
            catch (ArgumentException aex) 
            {
                _logger.LogInformation($"Exception Message: {aex.Message} | StackTrace: {aex.StackTrace}");
                return StatusCode(400, new { Message = aex.Message });
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new { Message = "Oops! Something went wrong!" });
            }
        }
    }
}

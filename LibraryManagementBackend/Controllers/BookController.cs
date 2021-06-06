using Entities;
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
        public IActionResult Post([FromBody]Book createBook)
        {
            try 
            {
                var bookName = createBook.Name;
                var authorId = createBook.AuthorID ?? 0;
                var publisherId = createBook.PublisherID ?? 0;
                var genreId = createBook.GenreID ?? 0;

                var book = _useCase.MakeObject(bookName, authorId, publisherId, genreId);

                book = _useCase.Save(book);

                return Ok(book);
            }
            catch (ArgumentException aex) 
            {
                _logger.LogInformation($"Exception Message: {aex.Message} | StackTrace: {aex.StackTrace}");
                return StatusCode(400, new { aex.Message });
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new { Message = "Oops! Something went wrong!" });
            }
        }
    }
}

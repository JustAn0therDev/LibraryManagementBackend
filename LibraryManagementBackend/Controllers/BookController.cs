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
        public ObjectResult Get()
        {
            try 
            {
                return Ok(_useCase.GetAll());
            }
            catch (ArgumentException anex)
            {
                return BadRequest(new { anex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPost]
        public ObjectResult Post([FromBody]Book createBook)
        {
            try 
            {
                var bookName = createBook.Name;
                var authorId = createBook.AuthorID;
                var publisherId = createBook.PublisherID;
                var genreId = createBook.GenreID;

                var book = _useCase.MakeObject(bookName, authorId, publisherId, genreId);

                book = _useCase.Save(book);

                return Created("", book);
            }
            catch (ArgumentException aex) 
            {
                _logger.LogInformation($"Exception Message: {aex.Message} | StackTrace: {aex.StackTrace}");
                return StatusCode(400, new { aex.Message });
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}

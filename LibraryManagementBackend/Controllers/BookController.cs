using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        /// <summary>
        /// Fetches all records of Book in the repository
        /// </summary>
        /// <returns>
        /// 200 if the list was successfully fetched, 
        /// 400 if something bad happened because of client parameters or
        /// header and 500 if an error happened in the server 
        /// </returns>
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


        /// <summary>
        /// Saves a book record in the repository
        /// </summary>
        /// <param name="bookParam">A book structure in JSON</param>
        /// <returns>
        /// 201 if the resource is successfully saved,
        /// 400 if something bad happened because of client parameters or
        /// header and 500 if an error happened in the server during saving 
        /// </returns>
        [HttpPost]
        public ObjectResult Post([FromBody]Book bookParam)
        {
            try 
            {
                var bookName = bookParam.Name;
                var authorId = bookParam.AuthorID;
                var publisherId = bookParam.PublisherID;
                var genreId = bookParam.GenreID;

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

using Entities;
using Microsoft.AspNetCore.Mvc;
using UseCases.Interfaces;
using System;

namespace LibraryManagementBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreUseCase _useCase;

        public GenreController(IGenreUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <summary>
        /// Fetches all records of Genre in the repository
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
        /// Saves a genre record in the repository
        /// </summary>
        /// <param name="genreParam">A genre structure in JSON</param>
        /// <returns>
        /// 201 if the resource is successfully saved,
        /// 400 if something bad happened because of client parameters or
        /// header and 500 if an error happened in the server during saving 
        /// </returns>
        [HttpPost]
        public ObjectResult Post([FromBody]Genre genreParam)
        {
            try
            {
                var genre = _useCase.MakeObject(genreParam.Name);

                return Created("", _useCase.Save(genre));
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
    }
}

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

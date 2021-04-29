using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UseCases.Interfaces;

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
        public IEnumerable<Genre> Get()
        {
            return _useCase.GetAll();
        }

        [HttpPost]
        public Genre Post([FromBody]Genre genreParam)
        {
            var genre = _useCase.MakeObject(genreParam.Name);

            return _useCase.Save(genre);
        }
    }
}

using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UseCases.Interfaces;

namespace LibraryManagementBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorUseCase _useCase;

        public AuthorController(IAuthorUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public IEnumerable<Author> Get()
        {
            return _useCase.GetAll();
        }

        [HttpPost]
        public Author Post([FromBody]Author authorParam)
        {
            var author = _useCase.MakeObject(authorParam.Name);

            return _useCase.Save(author);
        }
    }
}

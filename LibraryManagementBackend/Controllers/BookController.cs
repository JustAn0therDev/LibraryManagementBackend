using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UseCases;

namespace LibraryManagementBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookUseCase _useCase;

        public BookController(IBookUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _useCase.GetAll();
        }
    }
}

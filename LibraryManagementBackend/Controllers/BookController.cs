using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UseCases.Interfaces;

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

        [HttpPost]
        public Book Post([FromBody]Book bookParam)
        {
            return _useCase.Save(bookParam);
        }
    }
}

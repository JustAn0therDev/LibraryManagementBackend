using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UseCases.Interfaces;

namespace LibraryManagementBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherUseCase _useCase;

        public PublisherController(IPublisherUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public IEnumerable<Publisher> Get()
        {
            return _useCase.GetAll();
        }

        [HttpPost]
        public Publisher Post([FromBody]Publisher PublisherParam)
        {
            var publisher = _useCase.MakeObject(PublisherParam.Name);

            return _useCase.Save(publisher);
        }
    }
}

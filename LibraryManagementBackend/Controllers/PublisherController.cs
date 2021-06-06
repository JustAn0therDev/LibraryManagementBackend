using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public ObjectResult Post([FromBody]Publisher PublisherParam)
        {
            try 
            {
                var publisher = _useCase.MakeObject(PublisherParam.Name);
                return Created("", _useCase.Save(publisher));
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

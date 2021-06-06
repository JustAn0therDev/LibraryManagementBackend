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


        /// <summary>
        /// Fetches all records of Publisher in the repository
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
        /// Saves a publisher record in the repository
        /// </summary>
        /// <param name="publisherParam">A publisher structure in JSON</param>
        /// <returns>
        /// 201 if the resource is successfully saved,
        /// 400 if something bad happened because of client parameters or
        /// header and 500 if an error happened in the server during saving 
        /// </returns>
        [HttpPost]
        public ObjectResult Post([FromBody]Publisher publisherParam)
        {
            try 
            {
                var publisher = _useCase.MakeObject(publisherParam?.Name);
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

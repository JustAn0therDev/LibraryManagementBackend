using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
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

        /// <summary>
        /// Fetches all records of Author in the repository
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
        /// Saves an author record in the repository
        /// </summary>
        /// <param name="authorParam">An author structure in JSON</param>
        /// <returns>
        /// 201 if the resource is successfully saved,
        /// 400 if something bad happened because of client parameters or
        /// header and 500 if an error happened in the server during saving 
        /// </returns>
        [HttpPost]
        public ObjectResult Post([FromBody] Author authorParam)
        {
            try 
            {
                var author = _useCase.MakeObject(authorParam.Name);

                return Created("", _useCase.Save(author));
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

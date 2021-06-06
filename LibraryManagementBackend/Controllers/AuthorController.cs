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

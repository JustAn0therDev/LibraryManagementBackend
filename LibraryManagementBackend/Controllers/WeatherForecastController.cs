using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using UseCases;

namespace LibraryManagementBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUseCase _useCase;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUseCase useCase)
        {
            _logger = logger;
            _useCase = useCase;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var returnedValueFromUseCase = _useCase.UseCaseMethod();
            var summaryString = string.Empty;

            if (returnedValueFromUseCase != null)
            {
                summaryString = returnedValueFromUseCase.ToString();
            }

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaryString,

            })
            .ToArray();
        }
    }
}

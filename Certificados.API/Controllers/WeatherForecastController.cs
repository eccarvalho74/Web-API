using Certificados.API.Controllers.Core;
using Certificados.Domain.Core;
using Certificados.Domain.Core.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Certificados.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class WeatherForecastController : CustomController
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger) :base(logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var retorno = new ServiceResult<IEnumerable<WeatherForecast>>();

            var data =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            retorno.Data = data;

            retorno.AddError("001", "Erro");

            return Result(retorno);
        }


        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast bosta )
        {

            if (ModelState.IsValid)
            {
                var retorno = new ServiceResult<WeatherForecast>();
                retorno.Data = bosta;
                return Result(retorno);
            }

            return Result(null,404);
        }

        [HttpPost]
        [Route("PostWeatherForecastErro")]
        public IActionResult PostErro()
        {
            var retorno = new ServiceResult<bool>();

            throw new Exception("Erro proposital");

            retorno.Data = true;



            return Result(retorno);
        }

    }
}
using Insane.AspNet.Identity.Model1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Insane.WebApiLiveTests.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IOptionsSnapshot<IdentityOptions> options;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptionsSnapshot<IdentityOptions> _options)
        {
            _logger = logger;
            this.options = _options;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var x = options.Get("v1");
            var y = options.Get("v2");
            y.Tag = "v100";
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace AzureKeyVal01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];


        private readonly IConfiguration configuration;
        public WeatherForecastController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("obtener-secreto")]
        public string GetSecreto()
        {
            return configuration.GetValue<string>("LlaveAPI")!;
        }

        //Para usar Azure KeyVault se debe instalar el paquete
        //Azure.Extensions.AspNetCore.Configuration.Secrets 1.4.0 //para obtener los secretos de Azure App Service 1.5.2
        
        //Para authenticarme para poder obtener los secretos
        //Azure.Identity 1.13.2 --> 1.21.0
    }
}

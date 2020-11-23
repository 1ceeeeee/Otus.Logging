using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Otus.Service2.Controllers
{
  public class User
  {
    public string Name { get; set; }
    public int Id { get; set; }
  }

  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
      "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }

    [HttpGet("remote")]
    public async Task<IEnumerable<WeatherForecast>> GetFromRemote()
    {
      var httpClient = new HttpClient
      {
        BaseAddress = new Uri("https://localhost:5003")
      };

      var dictionary = new List<KeyValuePair<string, string>>
      {
        KeyValuePair.Create("City", "London")
      };

      using (_logger.BeginScope(dictionary))
      {
        _logger
          .WithProperty("City", "London")
          .LogInformation("User is {@User}",
            new User
            {
              Id = 1,
              Name = "Jon"
            });
      }
      
      var forecast = await httpClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>("/weatherforecast");

      return forecast;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
      var dictionary = new List<KeyValuePair<string, string>>
      {
        KeyValuePair.Create("City", "London")
      };

      using (_logger.BeginScope(dictionary))
      {
        _logger
          .WithProperty("City", "London")
          .LogInformation("User is {@User}",
            new User
            {
              Id = 1,
              Name = "Jon"
            });
      }

      var rng = new Random();
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
          Date = DateTime.Now.AddDays(index),
          TemperatureC = rng.Next(-20, 55),
          Summary = Summaries[rng.Next(Summaries.Length)]
        })
        .ToArray();
    }
  }
}
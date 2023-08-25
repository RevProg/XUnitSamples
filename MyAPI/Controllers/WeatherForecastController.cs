using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Services;

namespace MyAPI.Controllers;

[ApiController]
[Route("WeatherForecast")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IMyService _myService;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IMyService myService, ILogger<WeatherForecastController> logger)
    {
        _myService = myService;
        _logger = logger;
    }

    [HttpGet("Last5Days")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("Total")]
    public int Total()
    {
        return _myService.Total;
    }
}
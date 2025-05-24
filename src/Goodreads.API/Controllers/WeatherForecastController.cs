using Goodreads.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly UserManager<User> _userManager;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [EndpointSummary("Get Weather Forecasts")]
    [EndpointDescription("Retrieves a list of weather forecasts for the next 5 days.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    [HttpGet("test")]
    [EndpointSummary("Test Endpoint")]
    [EndpointDescription("A simple test endpoint to verify API functionality.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult TestEndpoint()
    {
        return Ok("Test endpoint is working!");
    }

    [HttpGet("users")]
    [EndpointSummary("Get All Users")]
    [EndpointDescription("Retrieves all users from the Identity store.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = _userManager.Users.ToList();
        return Ok(users);
    }

}

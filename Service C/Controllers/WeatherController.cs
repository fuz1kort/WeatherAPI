using Microsoft.AspNetCore.Mvc;
using Service_C.Interfaces;

namespace Service_C.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController(IWeatherService weatherService) : ControllerBase
{
    [HttpGet("latest")]
    public IActionResult GetLatestWeatherData()
    {
        var data = weatherService.GetLatestWeatherData();
        return Ok(data);
    }
}
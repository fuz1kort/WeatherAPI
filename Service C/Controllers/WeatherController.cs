using Microsoft.AspNetCore.Mvc;
using Service_C.Data;
using Service_C.Models;

namespace Service_C.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController(IWeatherStorage weatherStorage) : ControllerBase
{
    [HttpGet("latest")]
    public IActionResult GetLatestWeatherData()
    {
        var data = weatherStorage.GetLastWeatherData()
            .OrderByDescending(d => d.timestamp) // Сортировка по метке времени (от последних к первым)
            .Take(10) // Выбор последних 10 записей
            .Select(d => new WeatherData { WeatherJson = d.weatherJson, Timestamp = d.timestamp })
            .ToList();

        return Ok(data);
    }
}
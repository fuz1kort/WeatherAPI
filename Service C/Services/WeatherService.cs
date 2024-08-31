using Service_C.Data;
using Service_C.Interfaces;
using Service_C.Models;

namespace Service_C.Services;

public class WeatherService(IWeatherStorage weatherStorage) : IWeatherService
{
    public List<WeatherData> GetLatestWeatherData()
    {
        var data = weatherStorage.GetLastWeatherData()
            .OrderByDescending(d => d.timestamp)
            .Take(10) 
            .Select(d => new WeatherData { WeatherJson = d.weatherJson, Timestamp = d.timestamp })
            .ToList();

        return data;
    }
}
using Service_C.Models;

namespace Service_C.Interfaces;

public interface IWeatherService
{
    List<WeatherData> GetLatestWeatherData();
}
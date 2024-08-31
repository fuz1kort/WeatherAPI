namespace Service_A.Interfaces;

public interface IWeatherProvider
{
    Task<string> GetWeatherAsync();
}
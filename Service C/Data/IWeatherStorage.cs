namespace Service_C.Data;

public interface IWeatherStorage
{
    void AddWeatherData(string data);
    IEnumerable<(string weatherJson, DateTime timestamp)> GetLastWeatherData();
}
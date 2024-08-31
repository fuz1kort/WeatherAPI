namespace Service_C.Data;

public interface IWeatherStorage
{
    void AddWeatherData(string data);
    List<(string weatherJson, DateTime timestamp)> GetLastWeatherData();
}
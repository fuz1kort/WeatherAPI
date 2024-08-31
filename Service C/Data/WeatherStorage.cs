namespace Service_C.Data;

public class WeatherStorage : IWeatherStorage
{
    private readonly Queue<(string weatherJson, DateTime timestamp)> _weatherDataQueue = new();

    public void AddWeatherData(string data)
    {
        _weatherDataQueue.Enqueue((data, DateTime.UtcNow));
        if (_weatherDataQueue.Count > 10) _weatherDataQueue.Dequeue();
    }

    public IEnumerable<(string weatherJson, DateTime timestamp)> GetLastWeatherData() 
        => _weatherDataQueue.ToList();
}
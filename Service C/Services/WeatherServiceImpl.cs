using Grpc.Core;
using Service_C.Data;
using WeatherService;

namespace Service_C.Services;

public class WeatherServiceImpl(IWeatherStorage weatherStorage) : Weather.WeatherBase
{
    public override Task<SetWeatherResponse> SetWeather(SetWeatherRequest request, ServerCallContext context)
    {
        weatherStorage.AddWeatherData(request.WeatherJson);
        return Task.FromResult(new SetWeatherResponse { Message = "Weather data saved successfully" });
    }
}
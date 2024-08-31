using Microsoft.Extensions.Options;
using Service_A.Configurations;
using Service_A.Interfaces;

namespace Service_A.Services;

public class WeatherProvider(IOptions<WeatherApiOptions> weatherApiOptions, HttpClient httpClient)
    : IWeatherProvider
{
    private readonly WeatherApiOptions _weatherApiOptions = weatherApiOptions.Value;

    public async Task<string> GetWeatherAsync()
    {
        var requestUri = $"{_weatherApiOptions.BaseUrl}?q={_weatherApiOptions.City}&appid={_weatherApiOptions.ApiKey}";
        var response = await httpClient.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync();

        throw new HttpRequestException($"Error fetching weather data: {response.StatusCode}");
    }
}
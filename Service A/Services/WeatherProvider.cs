using Service_A.Interfaces;

namespace Service_A.Services;

public class WeatherProvider(HttpClient httpClient) : IWeatherProvider
{
    private const string ApiKey = "9143fcde774db2b32947bf720db3d4d2";
    private const string City = "Kazan";

    public async Task<string> GetWeatherAsync()
    {
        const string url = $"http://api.openweathermap.org/data/2.5/weather?q={City}&appid={ApiKey}";
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync();

        throw new HttpRequestException($"Error fetching weather data: {response.StatusCode}");
    }
}
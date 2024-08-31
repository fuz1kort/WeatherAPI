namespace Service_A.Configurations;

public class WeatherApiOptions
{
    public string BaseUrl { get; set; } = default!;
    public string City { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
}
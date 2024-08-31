using Service_A.Interfaces;

namespace Service_A.Services;

public class WeatherProducerService(IWeatherProvider weatherProvider, IKafkaProducer kafkaProducer)
    : BackgroundService
{
    private const string WeatherTopic = "weather";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var weatherData = await weatherProvider.GetWeatherAsync();
            await kafkaProducer.ProduceAsync(WeatherTopic, weatherData);

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
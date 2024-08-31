using Microsoft.Extensions.Options;
using Service_A.Configurations;
using Service_A.Interfaces;

namespace Service_A.Services;

public class WeatherProducerService(
    IWeatherProvider weatherProvider,
    IKafkaProducer kafkaProducer,
    IOptions<KafkaOptions> kafkaOptions)
    : BackgroundService
{
    private readonly KafkaOptions _kafkaOptions = kafkaOptions.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var weatherData = await weatherProvider.GetWeatherAsync();
            await kafkaProducer.ProduceAsync(_kafkaOptions.WeatherTopic, weatherData);

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
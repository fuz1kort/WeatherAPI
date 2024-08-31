using Service_B.Interfaces;

namespace Service_B.Services;

public class WeatherConsumerService(IWeatherConsumer weatherConsumer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) 
        => await weatherConsumer.ConsumeAsync(stoppingToken);
}
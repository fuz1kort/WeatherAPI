using Confluent.Kafka;
using Grpc.Net.Client;
using Service_B.Interfaces;
using WeatherService;

namespace Service_B.Services;

public class WeatherConsumer : IWeatherConsumer
{
    private readonly IConsumer<Null, string> _consumer;
    private readonly Weather.WeatherClient _grpcClient;

    public WeatherConsumer()
    {
        var config = new ConsumerConfig
        {
            GroupId = "weather-consumer-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Null, string>(config).Build();
        _grpcClient = new Weather.WeatherClient(GrpcChannel.ForAddress("https://localhost:7031"));
    }

    public async Task ConsumeAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe("weather");

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(TimeSpan.FromMilliseconds(100));
            
                if (consumeResult != null)
                {
                    var weatherData = consumeResult.Message.Value;

                    await _grpcClient.SetWeatherAsync(
                        new SetWeatherRequest { WeatherJson = weatherData }, 
                        cancellationToken: cancellationToken
                    );
                }
            }
            catch (ConsumeException ex)
            {
                Console.WriteLine($"Ошибка при потреблении сообщения: {ex.Error.Reason}");
            }
        
            await Task.Delay(500, cancellationToken);
        }
    }
}
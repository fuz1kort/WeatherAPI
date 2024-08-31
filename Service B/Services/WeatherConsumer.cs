using Confluent.Kafka;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Service_B.Configurations;
using Service_B.Interfaces;
using WeatherService;

namespace Service_B.Services;

public class WeatherConsumer : IWeatherConsumer
{
    private readonly IConsumer<Null, string> _consumer;
    private readonly Weather.WeatherClient _grpcClient;
    private readonly KafkaOptions _kafkaOptions;

    public WeatherConsumer(IOptions<KafkaOptions> kafkaOptions, IOptions<ExternalServicesOptions> externalServicesOptions)
    {
        _kafkaOptions = kafkaOptions.Value;
        var externalServicesOptions1 = externalServicesOptions.Value;
        var config = new ConsumerConfig
        {
            GroupId = _kafkaOptions.GroupId,
            BootstrapServers = _kafkaOptions.BootstrapServers,
            AutoOffsetReset = Enum.Parse<AutoOffsetReset>(_kafkaOptions.AutoOffsetReset, ignoreCase: true)
        };

        _consumer = new ConsumerBuilder<Null, string>(config).Build();
        _grpcClient = new Weather.WeatherClient(GrpcChannel.ForAddress(externalServicesOptions1.ServiceC.BaseUrl));
    }

    public async Task ConsumeAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe(_kafkaOptions.WeatherTopic);

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
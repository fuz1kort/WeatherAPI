namespace Service_B.Configurations;

public class KafkaOptions
{
    public string GroupId { get; set; } = default!;
    public string BootstrapServers { get; set; } = default!;
    public string AutoOffsetReset { get; set; } = default!;
    public string WeatherTopic { get; set; } = default!;
}
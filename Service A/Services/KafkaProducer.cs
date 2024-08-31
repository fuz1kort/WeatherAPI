using Confluent.Kafka;
using Service_A.Interfaces;

namespace Service_A.Services;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer(string kafkaBroker)
    {
        var config = new ProducerConfig { BootstrapServers = kafkaBroker };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceAsync(string topic, string message) 
        => await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
}
namespace Service_A.Interfaces;

public interface IKafkaProducer
{
    Task ProduceAsync(string topic, string message);
}
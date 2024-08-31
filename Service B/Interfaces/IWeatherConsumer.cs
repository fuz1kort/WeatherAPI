namespace Service_B.Interfaces;

public interface IWeatherConsumer
{
    Task ConsumeAsync(CancellationToken cancellationToken);
}
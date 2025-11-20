using Confluent.Kafka;
using System.Text.Json;

namespace Coworking.Reservas.Api.Kafka;

public class KafkaProducerService
{
    private readonly IProducer<string, string> _producer;

    public KafkaProducerService(IConfiguration configuration)
    {
        var config = new ProducerConfig
        {
            //usamos el hostname de Docker, no localhost
            BootstrapServers = configuration["Kafka:BootstrapServers"] ?? "kafka:9092",
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task SendAsync(string topic, object message)
    {
        var json = JsonSerializer.Serialize(message);

        await _producer.ProduceAsync(topic, new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = json
        });
    }
}
using System.Text.Json;
using Confluent.Kafka;
using OrderProcessingSystem.Application.Orders.Events;

namespace OrderProcessingSystem.Infrastructure.Messaging;

public class KafkaOrderEventPublisher : IOrderEventPublisher
{
    private readonly string TopicName = "orders.created";
    private readonly IProducer<string, string> _producer;
    public KafkaOrderEventPublisher()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }
    public async Task PublishOrderCreatedAsync(
        OrderCreatedEvent orderCreatedEvent,
        CancellationToken cancellationToken
    )
    {
        var json = JsonSerializer.Serialize(orderCreatedEvent);

        var message = new Message<string, string>
        {
            Key = orderCreatedEvent.OrderId.ToString(),
            Value = json
        };

        await _producer.ProduceAsync(TopicName, message, cancellationToken);
    }
}
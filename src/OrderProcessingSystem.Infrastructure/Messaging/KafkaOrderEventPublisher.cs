using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using OrderProcessingSystem.Application.Orders.Events;

namespace OrderProcessingSystem.Infrastructure.Messaging;

public class KafkaOrderEventPublisher : IOrderEventPublisher
{
    private readonly KafkaOptions _options;
    private readonly IProducer<string, string> _producer;
    public KafkaOrderEventPublisher(IOptions<KafkaOptions> options)
    {
        _options = options.Value;
        var config = new ProducerConfig
        {
            BootstrapServers = _options.BootstrapServers
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

        await _producer.ProduceAsync(_options.OrderCreatedTopic,
            message,
            cancellationToken);
    }
}
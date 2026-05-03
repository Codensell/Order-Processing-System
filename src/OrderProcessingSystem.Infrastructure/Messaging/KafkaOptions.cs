namespace OrderProcessingSystem.Infrastructure.Messaging;

public class KafkaOptions
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string OrderCreatedTopic { get; set; } = string.Empty;
}
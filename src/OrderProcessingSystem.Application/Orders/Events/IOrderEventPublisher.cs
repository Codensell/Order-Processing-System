namespace OrderProcessingSystem.Application.Orders.Events;

public interface IOrderEventPublisher
{
    Task PublishOrderCreatedAsync(
        OrderCreatedEvent orderCreatedEvent,
        CancellationToken cancellationToken
    );
}
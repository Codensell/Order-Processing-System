namespace OrderProcessingSystem.Application.Orders.Events;

public class OrderCreatedEvent
{
    public Guid OrderId{get; set;}
    public DateTime CreatedAt{get; set;}
    public int ItemsCount {get; set;}
}
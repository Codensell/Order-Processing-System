using OrderProcessingSystem.Domain.Enums;

namespace OrderProcessingSystem.Domain.Entities;

public class Order
{
    public Guid Id {get; private set;}
    public DateTime CreatedAt {get; private set;}
    public OrderStatus Status {get; private set;}

    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items;

    private Order(){}

    public static Order Create()
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Created
        };
    }

    public void AddItem(string productName, decimal price, int quantity)
    {
        if(Status != OrderStatus.Created)
            throw new InvalidOperationException("Cannot modify order");
        
        var item = new OrderItem(productName, price, quantity);
        _items.Add(item);
    }
    public void Pay()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("Order must contain items");
        if(Status != OrderStatus.Created)
            throw new InvalidOperationException("Order cannot be paid");
        
        Status = OrderStatus.Paid;
    }
    public void Cancel()
    {
        if(Status == OrderStatus.Paid)
            throw new InvalidOperationException("Paid order cannot be cancelled");
        
        Status = OrderStatus.Cancelled;
    }
}

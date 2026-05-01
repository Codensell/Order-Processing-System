namespace OrderProcessingSystem.Domain.Entities;

public class OrderItem
{
    public Guid Id {get; private set;}
    public string? ProductName {get; private set;}
    public decimal Price {get; private set;}
    public int Quantity {get; private set;}

    private OrderItem()
    {
    }
    public OrderItem(string productName, decimal price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name is required");
        if(price < 0)
            throw new ArgumentException("Price must be greater than zero");
        if(quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");
        
        Id = Guid.NewGuid();
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }
}
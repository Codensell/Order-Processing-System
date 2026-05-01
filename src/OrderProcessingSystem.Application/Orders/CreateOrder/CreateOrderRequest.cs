namespace OrderProcessingSystem.Application.Orders.CreateOrder;

public class CreateOrderRequest
{
    public List<OrderItemDto> Items{get; set;} = [];

    public class OrderItemDto
    {
        public string ProductName {get; set;} = default!;
        public decimal Price {get; set;}
        public int Quantity {get; set;}
    }
}
using OrderProcessingSystem.Domain.Enums;

namespace OrderProcessingSystem.Application.Orders.GetOrderById;

public class GetOrderByIdResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public List<GetOrderByIdItemResponse> Items { get; set; } = [];
}

public class GetOrderByIdItemResponse
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
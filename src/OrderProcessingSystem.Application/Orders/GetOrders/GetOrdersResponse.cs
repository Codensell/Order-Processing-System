using OrderProcessingSystem.Domain.Enums;

namespace OrderProcessingSystem.Application.Orders.GetOrders;

public class GetOrdersResponse
{
    public List<GetOrdersItemResponse> Orders { get; set; } = [];
}

public class GetOrdersItemResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public int ItemsCount { get; set; }
}
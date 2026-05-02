namespace OrderProcessingSystem.Application.Orders.GetOrders;

public interface IGetOrdersService
{
    Task<GetOrdersResponse> ExecuteAsync(CancellationToken cancellationToken);
}
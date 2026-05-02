namespace OrderProcessingSystem.Application.Orders.CreateOrder;
public interface ICreateOrderService
{
    Task<CreateOrderResponse> ExecuteAsync(CreateOrderRequest request, CancellationToken cancellationToken);
}
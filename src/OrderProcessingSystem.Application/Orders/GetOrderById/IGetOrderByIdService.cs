namespace OrderProcessingSystem.Application.Orders.GetOrderById;

public interface IGetOrderByIdService
{
    Task<GetOrderByIdResponse?> ExecuteAsync(Guid id, CancellationToken cancellationToken);
}
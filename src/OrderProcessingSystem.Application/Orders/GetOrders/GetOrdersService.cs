using OrderProcessingSystem.Application.Orders;

namespace OrderProcessingSystem.Application.Orders.GetOrders;

public class GetOrdersService : IGetOrdersService
{
    private readonly IOrderRepository _repository;

    public GetOrdersService(IOrderRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetOrdersResponse> ExecuteAsync(CancellationToken cancellationToken)
    {
        var orders = await _repository.GetAllAsync(cancellationToken);

        return new GetOrdersResponse
        {
            Orders = orders.Select(order => new GetOrdersItemResponse
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                ItemsCount = order.Items.Count
            }).ToList()
        };
    }
}
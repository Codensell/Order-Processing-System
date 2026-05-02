using System.Runtime.CompilerServices;
using OrderProcessingSystem.Application.Orders;

namespace OrderProcessingSystem.Application.Orders.GetOrderById;

public class GetOrderByIdService : IGetOrderByIdService
{
    private readonly IOrderRepository _repository;

    public GetOrderByIdService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetOrderByIdResponse?> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(id, cancellationToken);

        if(order == null)
        {
            return null;
        }

        return new GetOrderByIdResponse
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            Status = order.Status,
            Items = order.Items.Select(item => new GetOrderByIdItemResponse
            {
                Id = item.Id,
                ProductName = item.ProductName!,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        };
    }
}
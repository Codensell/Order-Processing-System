using OrderProcessingSystem.Domain.Entities;
using OrderProcessingSystem.Application.Orders;

namespace OrderProcessingSystem.Application.Orders.CreateOrder;

public class CreateOrderService : ICreateOrderService
{
    private readonly IOrderRepository _repository;

    public CreateOrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateOrderResponse> ExecuteAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var order = Order.Create();

        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductName, item.Price, item.Quantity);
        }

        _repository.Add(order);
        await _repository.SaveChangesAsync(cancellationToken);

        return new CreateOrderResponse
        {
            OrderId = order.Id
        };
    }
}

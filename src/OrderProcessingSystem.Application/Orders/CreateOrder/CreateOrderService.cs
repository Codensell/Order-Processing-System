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

    public CreateOrderResponse Execute(CreateOrderRequest request)
    {
        var order = Order.Create();

        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductName, item.Price, item.Quantity);
        }

        _repository.Add(order);
        _repository.SaveChanges();

        return new CreateOrderResponse
        {
            OrderId = order.Id
        };
    }
}

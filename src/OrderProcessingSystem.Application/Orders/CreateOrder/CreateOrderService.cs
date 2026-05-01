using OrderProcessingSystem.Domain.Entities;

namespace OrderProcessingSystem.Application.Orders.CreateOrder;

public class CreateOrderService
{
    public CreateOrderResponse Execute(CreateOrderRequest request)
    {
        var order = Order.Create();

        foreach(var item in request.Items)
        {
            order.AddItem(item.ProductName, item.Price, item.Quantity);
        }

        return new CreateOrderResponse
        {
            OrderId = order.Id
        };
    }
}

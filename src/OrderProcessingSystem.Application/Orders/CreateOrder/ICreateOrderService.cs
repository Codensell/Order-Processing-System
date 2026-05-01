using OrderProcessingSystem.Application.Orders.CreateOrder;

public interface ICreateOrderService
{
    CreateOrderResponse Execute(CreateOrderRequest request);
}
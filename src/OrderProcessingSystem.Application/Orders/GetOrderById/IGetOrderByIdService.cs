namespace OrderProcessingSystem.Application.Orders.GetOrderById;

public interface IGetOrderByIdService
{
    GetOrderByIdResponse? Execute(Guid id);
}
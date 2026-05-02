using OrderProcessingSystem.Domain.Entities;

namespace OrderProcessingSystem.Application.Orders;
public interface IOrderRepository
{
    void Add(Order order);
    Order? GetById(Guid id);
    void SaveChanges();
}
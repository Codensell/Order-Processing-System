using OrderProcessingSystem.Domain.Entities;

namespace OrderProcessingSystem.Application.Orders;
public interface IOrderRepository
{
    void Add(Order order);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
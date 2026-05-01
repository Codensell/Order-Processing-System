using OrderProcessingSystem.Application.Orders;
using OrderProcessingSystem.Domain.Entities;

namespace OrderProcessingSystem.Infrastructure.Data;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Order order)
    {
        _dbContext.Orders.Add(order);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}
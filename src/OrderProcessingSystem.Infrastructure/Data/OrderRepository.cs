using OrderProcessingSystem.Application.Orders;
using OrderProcessingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public Order? GetById(Guid id)
    {
        return _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefault(o => o.Id == id);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}
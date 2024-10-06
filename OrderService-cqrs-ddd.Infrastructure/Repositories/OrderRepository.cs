using OrderService_cqrs_ddd.Application.Repositories;
using OrderService_cqrs_ddd.Domain.Aggregates;
using OrderService_cqrs_ddd.Infrastructure.Persistence;

namespace OrderService_cqrs_ddd.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> GetByIdAsync(Guid orderId)
    {
        return await _dbContext.Orders.FindAsync(orderId);
    }

    public async Task SaveAsync(Order order)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid orderId)
    {
        var order = await _dbContext.Orders.FindAsync(orderId);
        if (order != null)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}

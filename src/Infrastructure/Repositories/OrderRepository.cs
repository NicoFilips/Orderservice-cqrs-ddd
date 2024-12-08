using OrderService.Application.Repositories;
using OrderService.Domain.Aggregates;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> GetByIdAsync(Guid orderId) => await _dbContext.Orders.FindAsync(orderId) ?? throw new InvalidOperationException($"Order with ID {orderId} was not found.");

    public async Task SaveAsync(Order order)
    {
        _ = await _dbContext.Orders.FindAsync(order.Id) == null
            ? await _dbContext.Orders.AddAsync(order)
            : _dbContext.Orders.Update(order);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid orderId)
    {
        Order? order = await _dbContext.Orders.FindAsync(orderId);
        if (order != null)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}

using OrderService.Domain.Aggregates;

namespace OrderService.Application.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid orderId);
    Task SaveAsync(Order order);
    Task DeleteAsync(Guid orderId);
}

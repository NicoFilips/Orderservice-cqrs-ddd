using OrderService_cqrs_ddd.Domain.Aggregates;

namespace OrderService_cqrs_ddd.Application.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid orderId);
    Task SaveAsync(Order order);
    Task DeleteAsync(Guid orderId);
}

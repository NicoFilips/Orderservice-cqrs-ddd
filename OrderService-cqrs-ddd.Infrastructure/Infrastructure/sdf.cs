namespace OrderService_cqrs_ddd.Infrastructure;

public interface IOrderRepository
{
    public async Task AddAsync(Order order);

    public async Task<Order> GetByIdAsync(Guid id);

    public async Task UpdateAsync(Order order);
}
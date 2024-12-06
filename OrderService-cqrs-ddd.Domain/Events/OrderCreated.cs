using OrderService_cqrs_ddd.Domain.Common;

namespace OrderService_cqrs_ddd.Domain.Events;

public class OrderCreated(Guid orderId) : IDomainEvent
{
    public Guid OrderId { get; } = orderId;
}

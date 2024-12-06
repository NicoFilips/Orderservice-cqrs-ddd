using OrderService_cqrs_ddd.Domain.Common;

namespace OrderService_cqrs_ddd.Domain.Events;

public class OrderCancelled(Guid orderId) : IDomainEvent
{
    public Guid OrderId { get; } = orderId;
}

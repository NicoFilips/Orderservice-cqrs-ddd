using OrderService.Domain.Common;

namespace OrderService.Domain.Events;

public class OrderCancelled(Guid orderId) : IDomainEvent
{
    public Guid OrderId { get; } = orderId;
}

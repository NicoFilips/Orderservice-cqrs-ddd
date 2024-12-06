using OrderService.Domain.Common;

namespace OrderService.Domain.Events;

public class OrderCreated(Guid orderId) : IDomainEvent
{
    public Guid OrderId { get; } = orderId;
}

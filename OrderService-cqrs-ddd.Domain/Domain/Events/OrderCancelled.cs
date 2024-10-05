using OrderService_cqrs_ddd.Domain.Common;

namespace OrderService_cqrs_ddd.Domain.Events;

public class OrderCancelled : IDomainEvent
{
    public Guid OrderId { get; }

    public OrderCancelled(Guid orderId)
    {
        OrderId = orderId;
    }
}
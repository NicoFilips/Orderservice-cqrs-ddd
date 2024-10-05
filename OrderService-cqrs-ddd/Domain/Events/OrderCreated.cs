using OrderService_cqrs_ddd.Domain.Common;

namespace OrderService_cqrs_ddd.Domain.Events;

public class OrderCreated : IDomainEvent

{
    public Guid OrderId { get; }

    public OrderCreated(Guid orderId)
    {
        OrderId = orderId;
    }
}
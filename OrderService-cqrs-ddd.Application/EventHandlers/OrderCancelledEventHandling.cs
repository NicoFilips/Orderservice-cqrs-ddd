using MediatR;
using OrderService_cqrs_ddd.Domain.Events;

namespace OrderService_cqrs_ddd.Application.EventHandlers;

public sealed class OrderCancelledEventHandling : INotificationHandler<OrderCreated>
{
    public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Order {notification.OrderId} was cancelled.");
        return Task.CompletedTask;
    }
}

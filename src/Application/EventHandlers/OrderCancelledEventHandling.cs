using MediatR;
using OrderService.Domain.Events;

namespace OrderService.Application.EventHandlers;

public sealed class OrderCancelledEventHandling : INotificationHandler<OrderCreated>
{
    public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Order {notification.OrderId} was cancelled.");
        return Task.CompletedTask;
    }
}

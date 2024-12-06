using MediatR;
using OrderService.Domain.Events;

namespace OrderService.Application.EventHandlers;

internal sealed class OrderCreatedEventHandler : INotificationHandler<OrderCreated>
{
    public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Order {notification.OrderId} was created.");
        return Task.CompletedTask;
    }
}

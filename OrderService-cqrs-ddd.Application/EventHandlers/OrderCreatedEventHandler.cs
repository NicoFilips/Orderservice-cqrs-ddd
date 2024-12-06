using MediatR;
using OrderService_cqrs_ddd.Domain.Events;

namespace OrderService_cqrs_ddd.Application.EventHandlers;

internal sealed class OrderCreatedEventHandler : INotificationHandler<OrderCreated>
{
    public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Order {notification.OrderId} was created.");
        return Task.CompletedTask;
    }
}

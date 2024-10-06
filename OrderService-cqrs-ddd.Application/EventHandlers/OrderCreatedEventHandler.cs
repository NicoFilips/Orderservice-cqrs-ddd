using MediatR;
using OrderService_cqrs_ddd.Domain.Events;

namespace MyApp.Application.EventHandlers;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreated>
{
    public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Order {notification.OrderId} was created.");
        return Task.CompletedTask;
    }
}

using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
    public required OrderItem Item { get; set; }
}

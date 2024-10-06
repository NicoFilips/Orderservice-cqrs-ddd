using MediatR;
using OrderService_cqrs_ddd.Domain.Entities;

namespace OrderService_cqrs_ddd.Application.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
    public List<OrderItem> Items { get; set; }
}

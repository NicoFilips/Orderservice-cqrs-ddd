using MediatR;

namespace OrderService_cqrs_ddd.Application.Commands;

public class CreateOrderCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
    public List<OrderItemDto> Items { get; set; }
}
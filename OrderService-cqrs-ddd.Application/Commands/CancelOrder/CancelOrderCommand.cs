using MediatR;

namespace OrderService_cqrs_ddd.Application.Commands.CancelOrder;

public class CancelOrderCommand : IRequest<Unit>
{
    public Guid OrderId { get; set; }
}

using MediatR;

namespace OrderService.Application.Commands.CancelOrder;

public class CancelOrderCommand : IRequest<Unit>
{
    public Guid OrderId { get; set; }
}

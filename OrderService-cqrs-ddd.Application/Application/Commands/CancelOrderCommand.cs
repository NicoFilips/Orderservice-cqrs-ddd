using MediatR;

namespace OrderService_cqrs_ddd.Application.Commands;

public class CancelOrderCommand : IRequest
{
    public Guid OrderId { get; set; }
}
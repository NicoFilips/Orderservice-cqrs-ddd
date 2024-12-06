using MediatR;
using OrderService_cqrs_ddd.Application.Repositories;
using OrderService_cqrs_ddd.SharedKernel.Exceptions;

namespace OrderService_cqrs_ddd.Application.Commands.CancelOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;

    public CancelOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        Domain.Aggregates.Order order = await _orderRepository.GetByIdAsync(request.OrderId) ?? throw new NotFoundException($"Order with ID {request.OrderId} not found.");
        order.Cancel();

        await _orderRepository.SaveAsync(order);

        return Unit.Value;
    }
}

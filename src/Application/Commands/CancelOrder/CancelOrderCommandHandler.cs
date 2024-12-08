using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.Repositories;
using OrderService.Domain.Aggregates;
using OrderService.SharedKernel.Exceptions;

namespace OrderService.Application.Commands.CancelOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CancelOrderCommandHandler> _logger;

    public CancelOrderCommandHandler(IOrderRepository orderRepository, ILogger<CancelOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = await _orderRepository.GetByIdAsync(request.OrderId) ?? throw new NotFoundException($"Order with ID {request.OrderId} not found."); // Exception sollte hier nicht aus der Domain kommen, da technischer Fehler
        order.Cancel();

        await _orderRepository.SaveAsync(order);

        _logger.LogInformation("Order {OrderId} canceled.", order.Id);
        return Unit.Value;
    }
}

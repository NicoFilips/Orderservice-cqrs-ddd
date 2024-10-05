using MediatR;
using OrderService_cqrs_ddd.Infrastructure;

namespace OrderService_cqrs_ddd.Application.Commands.Handlers;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public CancelOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        // Die Bestellung mit der übergebenen OrderId laden
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order == null)
        {
            throw new NotFoundException($"Order with ID {request.OrderId} not found.");
        }

        // Geschäftslogik zum Stornieren der Bestellung ausführen
        order.Cancel();  // Annahme: Es gibt eine Cancel-Methode in der Order-Entität

        // Änderungen speichern
        await _orderRepository.SaveAsync(order);

        // Rückgabe von Unit, da der Command keine Rückgabe erwartet
        return Unit.Value;
    }
}

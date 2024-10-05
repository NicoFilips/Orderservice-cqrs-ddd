using MediatR;
using OrderService_cqrs_ddd.Application.Repositories;
using OrderService_cqrs_ddd.Domain.Aggregates;

namespace OrderService_cqrs_ddd.Application.Commands.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryService _inventoryService;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IInventoryService inventoryService)
    {
        _orderRepository = orderRepository;
        _inventoryService = inventoryService;
    }

    public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order(command.CustomerId, command.Items);

        // Artikel reservieren
        await _inventoryService.ReserveItems(command.Items);

        // Bestellung speichern
        await _orderRepository.SaveAsync(order);

        return order.Id;  // Rückgabe der Order ID
    }
}

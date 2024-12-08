using MediatR;
using OrderService.Application.Repositories;
using OrderService.Domain.Aggregates;

namespace OrderService.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryRepository _inventoryRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IInventoryRepository inventoryRepository)
    {
        _orderRepository = orderRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerId, request.Item);

        var productQuantity = new KeyValuePair<Guid, int>(request.Item.ProductId, request.Item.Quantity);

        await _inventoryRepository.ReserveItemsAsync(productQuantity);

        await _orderRepository.SaveAsync(order);

        return order.Id;
    }
}

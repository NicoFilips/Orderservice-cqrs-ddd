using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.Exceptions;
using OrderService.Application.Repositories;
using OrderService.Domain.Aggregates;
using Exception = System.Exception;

namespace OrderService.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IInventoryRepository inventoryRepository, ILogger<CreateOrderCommandHandler> logger)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = new Order(request.CustomerId, request.Item);
            var productQuantity = new KeyValuePair<Guid, int>(request.Item.ProductId, request.Item.Quantity);
            await _inventoryRepository.ReserveItemsAsync(productQuantity);
            await _orderRepository.SaveAsync(order);
            _logger.LogInformation("Order {OrderId} created.", order.Id);
            return order.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create order.");
            throw new DatabaseUnavailableException("Failed to create order.", ex);
        }
    }
}

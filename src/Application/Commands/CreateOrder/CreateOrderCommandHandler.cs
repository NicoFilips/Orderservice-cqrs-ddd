﻿using MediatR;
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
        var order = new Order(request.CustomerId, request.Items);

        var productQuantities = request.Items.ToDictionary(item => item.ProductId, item => item.Quantity);

        await _inventoryRepository.ReserveItemsAsync(productQuantities);

        await _orderRepository.SaveAsync(order);

        return order.Id;
    }
}

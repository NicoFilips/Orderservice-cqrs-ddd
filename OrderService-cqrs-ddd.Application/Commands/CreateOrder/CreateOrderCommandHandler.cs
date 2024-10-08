﻿using MediatR;
using OrderService_cqrs_ddd.Application.Repositories;
using OrderService_cqrs_ddd.Domain.Aggregates;

namespace OrderService_cqrs_ddd.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryRepository _inventoryRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IInventoryRepository inventoryRepository)
    {
        _orderRepository = orderRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order(command.CustomerId, command.Items);

        var productQuantities = command.Items.ToDictionary(item => item.ProductId, item => item.Quantity);

        await _inventoryRepository.ReserveItemsAsync(productQuantities);

        await _orderRepository.SaveAsync(order);

        return order.Id;
    }
}

﻿using Grpc.Core;
using MediatR;
using OrderService.API.Grpc;
using OrderService.Application.Commands.CancelOrder;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Domain.Entities;

namespace OrderService.API.Endpoints.GRPC;

public class OrdersGrpc : OrderService.API.Grpc.OrderService.OrderServiceBase
{
    private readonly IMediator _mediator;

    public OrdersGrpc(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
    {
        var command = new CreateOrderCommand
        {
            CustomerId = Guid.Parse(request.CustomerId),
            Items = request.Items.Select(item => new OrderItem
            {
                ProductId = Guid.Parse(item.ProductId),
                Quantity = item.Quantity
            }).ToList()
        };

        Guid orderId = await _mediator.Send(command);

        return new CreateOrderResponse
        {
            OrderId = orderId.ToString() // Konvertiere Guid zu string
        };
    }

    public override async Task<CancelOrderResponse> CancelOrder(CancelOrderRequest request, ServerCallContext context)
    {
        var command = new CancelOrderCommand
        {
            OrderId = Guid.Parse(request.OrderId)
        };

        await _mediator.Send(command);

        return new CancelOrderResponse
        {
            Message = $"Order {request.OrderId} has been canceled."
        };
    }
}

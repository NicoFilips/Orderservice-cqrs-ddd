using Grpc.Core;
using MediatR;
using OrderService_cqrs_ddd.API.Grpc;
using OrderService_cqrs_ddd.Application.Commands.CancelOrder;
using OrderService_cqrs_ddd.Application.Commands.CreateOrder;
using OrderService_cqrs_ddd.Domain.Entities;

namespace OrderService_cqrs_ddd.API.Endpoints.GRPC;

public class OrdersGrpc : OrderService.OrderServiceBase
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

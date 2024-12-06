using MediatR;
using OrderService_cqrs_ddd.Application.Commands.CancelOrder;
using OrderService_cqrs_ddd.Application.Commands.CreateOrder;

namespace OrderService_cqrs_ddd.API.Endpoints.MinimalAPI;

public static class OrdersMinimalApi
{
    public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // POST: /api/orders
        endpoints.MapPost("/api/orders", async (CreateOrderCommand command, IMediator mediator) =>
        {
            Guid result = await mediator.Send(command);
            return Results.Ok(result);
        });

        // DELETE: /api/orders/{id}
        endpoints.MapDelete("/api/orders/{id}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new CancelOrderCommand { OrderId = id });
            return Results.NoContent();
        });

        return endpoints;
    }
}

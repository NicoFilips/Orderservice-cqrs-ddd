using MediatR;
using OrderService.Application.Commands.CancelOrder;
using OrderService.Application.Commands.CreateOrder;

namespace OrderService.API.Endpoints.MinimalApi;

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

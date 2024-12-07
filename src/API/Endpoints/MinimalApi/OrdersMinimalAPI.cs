using MediatR;
using OrderService.Application.Commands.CancelOrder;
using OrderService.Application.Commands.CreateOrder;

namespace OrderService.API.Endpoints.MinimalApi;

public static class OrdersMinimalApi
{
    public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // POST: /minimal-api/orders
        endpoints.MapPost(
                      "/minimal-api/orders",
                      async (CreateOrderCommand command, IMediator mediator) =>
                      {
                          Guid result = await mediator.Send(command);
                          return Results.Ok(result);
                      })
                 .WithTags("Orders")
                 .WithName("CreateOrder")
                 .WithOpenApi();

        // DELETE: /minimal-api/orders/{id}
        endpoints.MapDelete(
                      "/minimal-api/orders/{id}",
                      async (Guid id, IMediator mediator) =>
                      {
                          await mediator.Send(new CancelOrderCommand { OrderId = id });
                          return Results.NoContent();
                      })
                 .WithTags("Orders")
                 .WithName("CancelOrder")
                 .WithOpenApi();
        return endpoints;
    }
}

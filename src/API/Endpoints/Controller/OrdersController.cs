using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands.CancelOrder;
using OrderService.Application.Commands.CreateOrder;

namespace OrderService.API.Endpoints.Controller;

[ApiController]
[Route("api-controller/[controller]")]
[Produces("application/json")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public OrdersController(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        _logger.LogInformation("Creating order for customer {CustomerId}.", command.CustomerId);
        Guid result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelOrder(CancelOrderCommand command)
    {
        _logger.LogInformation("Canceling order {OrderId}.", command.OrderId);
        await _mediator.Send(command);
        return NoContent();
    }
}

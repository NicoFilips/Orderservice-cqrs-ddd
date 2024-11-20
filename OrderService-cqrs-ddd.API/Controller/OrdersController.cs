using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService_cqrs_ddd.Application.Commands.CancelOrder;
using OrderService_cqrs_ddd.Application.Commands.CreateOrder;

namespace OrderService_cqrs_ddd.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly string ntype = "OrdersController";

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        await _mediator.Send(new CancelOrderCommand { OrderId = id });
        return NoContent();
    }
}

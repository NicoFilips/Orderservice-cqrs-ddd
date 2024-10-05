using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService_cqrs_ddd.Application.Commands;

namespace OrderService_cqrs_ddd.API;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result); // Returns the created Order ID
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        await _mediator.Send(new CancelOrderCommand { OrderId = id });
        return NoContent();  // Gibt einen 204-Statuscode zurück, was bedeutet, dass die Anfrage erfolgreich war, aber keine Daten zurückgegeben werden.
    }
}

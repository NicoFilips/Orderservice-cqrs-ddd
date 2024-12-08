namespace OrderService.API.DTOs;

public record OrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

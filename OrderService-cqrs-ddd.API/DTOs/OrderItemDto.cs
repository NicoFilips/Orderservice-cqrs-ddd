namespace OrderService_cqrs_ddd.API.DTOs;

public record OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

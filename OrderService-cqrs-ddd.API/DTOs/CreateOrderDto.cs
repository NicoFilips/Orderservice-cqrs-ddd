namespace OrderService_cqrs_ddd.API.DTOs;

public class CreateOrderDto(List<OrderItemDto> items)
{
    public Guid CustomerId { get; set; }

    public List<OrderItemDto> Items { get; set; } = items;
}

namespace OrderService_cqrs_ddd.API.DTOs;

public class CreateOrderDto
{
    public Guid CustomerId { get; set; }

    public List<OrderItemDto> Items { get; set; }
}

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

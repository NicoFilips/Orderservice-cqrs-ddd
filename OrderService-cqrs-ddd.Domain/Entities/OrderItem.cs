namespace OrderService_cqrs_ddd.Domain.Entities;

public class OrderItem
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal TotalPrice => Quantity * UnitPrice;

    public OrderItem(int quantity, decimal unitPrice, Guid productId = default)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
    public OrderItem(int quantity, decimal unitPrice)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
    public OrderItem() { throw new NotImplementedException(); }
}

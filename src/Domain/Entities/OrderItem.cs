namespace OrderService.Domain.Entities;

public class OrderItem
{
    public Guid ProductId { get; init; }
    public int Quantity { get; set; }
    public double UnitPrice { get; init; }
    public double TotalPrice => Quantity * UnitPrice;

    public OrderItem(int quantity, double unitPrice, Guid productId = default)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
    public OrderItem(int quantity, double unitPrice)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
    public OrderItem()
    {
        ProductId = Guid.Empty;
        Quantity = 0;
        UnitPrice = 0;
    }
}

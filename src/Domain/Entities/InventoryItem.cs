namespace OrderService.Domain.Entities;

public class InventoryItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public double UnitPrice { get; set; }

    public InventoryItem(Guid productId, int quantity, double unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public void ReduceQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Amount is less than zero.");

        if (Quantity < quantity)
            throw new InvalidOperationException($"Not enough stock for {ProductId}");

        Quantity -= quantity;
    }

    public void AddQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Amount must be greater than 0.");

        Quantity += quantity;
    }
}

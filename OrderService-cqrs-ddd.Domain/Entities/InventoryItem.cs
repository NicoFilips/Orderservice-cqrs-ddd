namespace OrderService_cqrs_ddd.Domain.Entities;

public class InventoryItem
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; set; }

    public InventoryItem(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public void ReduceQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Die Menge muss größer als 0 sein.");
        }

        if (Quantity < quantity)
        {
            throw new InvalidOperationException($"Nicht genügend Bestand für Produkt {ProductId}");
        }

        Quantity -= quantity;
    }

    public void AddQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Die Menge muss größer als 0 sein.");
        }
        Quantity += quantity;
    }
}

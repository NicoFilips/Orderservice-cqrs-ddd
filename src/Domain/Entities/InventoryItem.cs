namespace OrderService.Domain.Entities;

public class InventoryItem(Guid productId, int quantity)
{
    public Guid ProductId { get; private set; } = productId;
    public int Quantity { get; set; } = quantity;

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

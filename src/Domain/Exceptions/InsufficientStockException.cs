namespace OrderService.Domain.Exceptions;

public class InsufficientStockException : Exception
{
    public Guid ProductId { get; }
    public int RequestedQuantity { get; }

    public InsufficientStockException(Guid productId, int requestedQuantity)
        : base($"Not enough stock for product with ID {productId}. Requested: {requestedQuantity}.")
    {
        ProductId = productId;
        RequestedQuantity = requestedQuantity;
    }
}

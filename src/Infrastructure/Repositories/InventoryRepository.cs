using Microsoft.EntityFrameworkCore;
using OrderService.Application.Repositories;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly AppDbContext _dbContext;

    public InventoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InventoryItem?> GetItemAsync(Guid productId) => await _dbContext.InventoryItems.FirstOrDefaultAsync(i => i.ProductId == productId);

    public async Task ReserveItemsAsync(KeyValuePair<Guid, int> productQuantities)
    {
        InventoryItem? inventoryItem = await GetItemAsync(productQuantities.Key) ?? throw new InsufficientStockException(
            productQuantities.Key,
            productQuantities.Value);

        if (inventoryItem.Quantity < productQuantities.Value)
            throw new InvalidOperationException($"Not enough stock for product with ID {productQuantities.Key}. Available: {inventoryItem.Quantity}, Requested: {productQuantities.Value}");

        inventoryItem.Quantity -= productQuantities.Value;
        _dbContext.InventoryItems.Update(inventoryItem);
        await _dbContext.SaveChangesAsync();
    }
}

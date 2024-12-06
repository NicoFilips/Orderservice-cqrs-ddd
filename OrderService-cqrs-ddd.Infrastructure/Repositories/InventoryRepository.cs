using Microsoft.EntityFrameworkCore;
using OrderService_cqrs_ddd.Application.Repositories;
using OrderService_cqrs_ddd.Domain.Entities;
using OrderService_cqrs_ddd.Infrastructure.Persistence;

namespace OrderService_cqrs_ddd.Infrastructure.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly AppDbContext _dbContext;

    public InventoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InventoryItem?> GetItemAsync(Guid productId) => await _dbContext.InventoryItems.FirstOrDefaultAsync(i => i.ProductId == productId);

    public async Task ReserveItemsAsync(Dictionary<Guid, int> productQuantities)
    {
        foreach (KeyValuePair<Guid, int> product in productQuantities)
        {
            InventoryItem? inventoryItem = await GetItemAsync(product.Key) ?? throw new InvalidOperationException($"Product with ID {product.Key} not found in inventory.");

            if (inventoryItem.Quantity < product.Value)
                throw new InvalidOperationException($"Not enough stock for product with ID {product.Key}. Available: {inventoryItem.Quantity}, Requested: {product.Value}");

            inventoryItem.Quantity -= product.Value;
            _dbContext.InventoryItems.Update(inventoryItem);
        }
        await _dbContext.SaveChangesAsync();
    }
}

using OrderService_cqrs_ddd.Application.Repositories;
using OrderService_cqrs_ddd.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OrderService_cqrs_ddd.Infrastructure.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly AppDbContext _dbContext;

    public InventoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InventoryItem> GetItemAsync(Guid productId)
    {
        return await _dbContext.InventoryItems.FirstOrDefaultAsync(i => i.ProductId == productId);
    }

    public async Task ReserveItemsAsync(List<OrderItem> items)
    {
        foreach (var item in items)
        {
            var inventoryItem = await GetItemAsync(item.ProductId);
            if (inventoryItem == null || inventoryItem.Quantity < item.Quantity)
            {
                throw new InvalidOperationException($"Nicht genügend Lagerbestand für Produkt {item.ProductId}");
            }

            // Reduziere den Bestand
            inventoryItem.Quantity -= item.Quantity;
            _dbContext.InventoryItems.Update(inventoryItem);
        }

        // Speichere die Änderungen in der Datenbank
        await _dbContext.SaveChangesAsync();
    }
}


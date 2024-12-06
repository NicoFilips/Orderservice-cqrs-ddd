using OrderService.Domain.Entities;

namespace OrderService.Application.Repositories;

public interface IInventoryRepository
{
    Task<InventoryItem?> GetItemAsync(Guid productId);

    Task ReserveItemsAsync(Dictionary<Guid, int> productQuantities);
}

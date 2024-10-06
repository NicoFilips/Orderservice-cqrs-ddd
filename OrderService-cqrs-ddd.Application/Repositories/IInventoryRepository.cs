using OrderService_cqrs_ddd.Domain.Entities;

namespace OrderService_cqrs_ddd.Application.Repositories;

public interface IInventoryRepository
{
    Task<InventoryItem> GetItemAsync(Guid productId);

    Task ReserveItemsAsync(Dictionary<Guid, int> productQuantities);
}


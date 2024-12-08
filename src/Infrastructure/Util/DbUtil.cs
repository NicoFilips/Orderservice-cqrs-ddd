using OrderService.Domain.Entities;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Util;

public static class DbUtil
{
    public static void SeedData(this AppDbContext dbContext)
    {
        dbContext.InventoryItems.Add(
            new InventoryItem(
                Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
                234,
                7.85));

        dbContext.InventoryItems.Add(
            new InventoryItem(
                Guid.Parse("123e4567-e89b-12d3-a456-426614174001"),
                23231,
                3.99));

        dbContext.InventoryItems.Add(
            new InventoryItem(
                Guid.Parse("123e4567-e89b-12d3-a456-426614174002"),
                12234,
                49.98));

        dbContext.InventoryItems.Add(
            new InventoryItem(
                Guid.Parse("123e4567-e89b-12d3-a456-426614174003"),
                32234,
                17.30));

        dbContext.InventoryItems.Add(
            new InventoryItem(
                Guid.Parse("123e4567-e89b-12d3-a456-426614174004"),
                100000,
                1.30));

        dbContext.SaveChanges();
    }
}

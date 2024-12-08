using OrderService.Domain.Aggregates;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Util;

public static class DbUtil
{
    // Inventory Item IDs
    private static string InvItem1Id { get; } = "123e4567-e89b-12d3-a456-426614174000";
    private static string InvItem2Id { get; } = "123e4567-e89b-12d3-a456-426614174001";
    private static string InvItem3Id { get; } = "123e4567-e89b-12d3-a456-426614174002";
    private static string InvItem4Id { get; } = "123e4567-e89b-12d3-a456-426614174003";
    private static string InvItem5Id { get; } = "123e4567-e89b-12d3-a456-426614174004";

    // Customer IDs
    private static string Customer1Id { get; } = "223e4567-e89b-12d3-a456-426614174100";

    // Order IDs
    private static string Order1Id { get; } = "323e4567-e89b-12d3-a456-426614174200";
    private static string Order2Id { get; } = "323e4567-e89b-12d3-a456-426614174201";
    private static string Order3Id { get; } = "323e4567-e89b-12d3-a456-426614174202";
    private static string Order4Id { get; } = "323e4567-e89b-12d3-a456-426614174203";
    private static string Order5Id { get; } = "323e4567-e89b-12d3-a456-426614174204";

    public static void SeedInventoryData(this AppDbContext dbContext)
    {
        dbContext.InventoryItems.Add(
            new InventoryItem(
                Guid.Parse(InvItem1Id),
                234,
                7.85));

        dbContext.InventoryItems.Add(
            new InventoryItem(
                Guid.Parse(InvItem2Id),
                23231,
                3.99));

        dbContext.InventoryItems.Add(
            new InventoryItem(
                Guid.Parse(InvItem3Id),
                12234,
                49.98));

        dbContext.InventoryItems.Add(
            new InventoryItem(
                Guid.Parse(InvItem4Id),
                32234,
                17.30));

        dbContext.InventoryItems.Add(
            new InventoryItem(
                Guid.Parse(InvItem5Id),
                100000,
                1.30));

        dbContext.SaveChanges();
    }

    public static void SeedOrderData(this AppDbContext dbContext)
    {
        dbContext.Orders.Add(
            new Order(
                Guid.Parse(Order1Id),
                new OrderItem(
                    5,
                    10.99,
                    Guid.Parse("123e4567-e89b-12d3-a456-426614174000")),
                "Created"));

        dbContext.Orders.Add(
            new Order(
                Guid.Parse(Order2Id),
                new OrderItem(
                    2,
                    20.49,
                    Guid.Parse("123e4567-e89b-12d3-a456-426614174001")),
                "Shipped"));

        dbContext.Orders.Add(
            new Order(
                Guid.Parse(Order3Id),
                new OrderItem(
                    1,
                    15.75,
                    Guid.Parse("123e4567-e89b-12d3-a456-426614174002")),
                "Created"));

        dbContext.Orders.Add(
            new Order(
                Guid.Parse(Order4Id),
                new OrderItem(
                    10, // Quantity
                    5.50, // UnitPrice
                    Guid.Parse("123e4567-e89b-12d3-a456-426614174003")),
                "Cancelled"));

        dbContext.Orders.Add(
            new Order(
                Guid.Parse(Order5Id),
                new OrderItem(
                    3,
                    7.80,
                    Guid.Parse("123e4567-e89b-12d3-a456-426614174004")),
                "Created"));

        dbContext.SaveChanges();
    }
}

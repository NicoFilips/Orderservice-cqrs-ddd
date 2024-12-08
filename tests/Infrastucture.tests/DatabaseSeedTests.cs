using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Util;

namespace OrderService.Infrastructure.Tests;

[TestFixture]
public class DatabaseSeedTests
{
    private AppDbContext _dbContext;

    [SetUp]
    public void SetUp()
    {
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                                .Options;

        _dbContext = new AppDbContext(options);

        _dbContext.SeedInventoryData();
        _dbContext.SeedOrderData();
    }

    [TearDown]
    public void TearDown() => _dbContext.Dispose();

    [Test]
    public void SeedInventoryData_Should_Add_InventoryItems_To_Database()
    {
        // Act
        var inventoryItems = _dbContext.InventoryItems.ToList();

        // Assert
        inventoryItems.Should().NotBeNull();
        inventoryItems.Should().HaveCount(5);
        inventoryItems.Select(i => i.ProductId).Should().OnlyHaveUniqueItems();
    }

    [Test]
    public void SeedOrderData_Should_Add_Orders_To_Database()
    {
        // Act
        var orders = _dbContext.Orders.ToList();

        // Assert
        orders.Should().NotBeNull();
        orders.Count.Should().BeGreaterThanOrEqualTo(1);
        orders.All(o => o.Status != string.Empty).Should().BeTrue("because every order should have a Status.");
    }
}

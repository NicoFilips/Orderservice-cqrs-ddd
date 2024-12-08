using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OrderService.Domain.Aggregates;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure.Tests;

[TestFixture]
public class OrderRepositoryTests
{
    [Test]
    public async Task GetById_Should_Throw_InvalidOperationException_If_Order_Not_Found()
    {
        // Arrange
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                                .Options;

        await using var dbContext = new AppDbContext(options);
        var orderRepository = new OrderRepository(dbContext);

        var nonExistentOrderId = Guid.NewGuid();

        // Act
        Func<Task> act = async () => await orderRepository.GetByIdAsync(nonExistentOrderId);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
                 .WithMessage($"Order with ID {nonExistentOrderId} was not found.");
    }

    [Test]
    public async Task GetById_Should_Return_Order_If_Found()
    {
        // Arrange
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                                .Options;

        await using var dbContext = new AppDbContext(options);
        var orderRepository = new OrderRepository(dbContext);

        var testOrder = new Order(
            Guid.NewGuid(),
            new OrderItem(5, 10.99, Guid.NewGuid()),
            "Created");

        dbContext.Orders.Add(testOrder);
        await dbContext.SaveChangesAsync();

        // Act
        Order retrievedOrder = await orderRepository.GetByIdAsync(testOrder.Id);

        // Assert
        retrievedOrder.Should().NotBeNull();
        retrievedOrder!.Id.Should().Be(testOrder.Id);
        retrievedOrder.Status.Should().Be(testOrder.Status);
    }

    [Test]
    public async Task GetById_Should_Throw_InvalidOperationException_If_Id_Is_Empty()
    {
        // Arrange
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                                .Options;

        await using var dbContext = new AppDbContext(options);
        var orderRepository = new OrderRepository(dbContext);

        // Act
        Func<Task> act = async () => await orderRepository.GetByIdAsync(Guid.Empty);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
                 .WithMessage("Order with ID 00000000-0000-0000-0000-000000000000 was not found.");
    }
}

using NUnit.Framework;
using FluentAssertions;
using OrderService_cqrs_ddd.Domain.Aggregates;
using OrderService_cqrs_ddd.Domain.Entities;
using OrderService_cqrs_ddd.Infrastructure.Persistence;
using OrderService_cqrs_ddd.Infrastructure.Repositories;

namespace OrderService_cqrs_ddd.Infrastructure.Tests;

[TestFixture]
public class OrderRepositoryTests
{
    private AppDbContext _dbContext;
    private OrderRepository _orderRepository;

    [SetUp]
    public void SetUp()
    {
        // Verwende die InMemory-Datenbank für jeden Test
        _dbContext = AppDbContextFactory.CreateInMemoryContext();
        _orderRepository = new OrderRepository(_dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose(); // DbContext nach jedem Test entsorgen
    }

    [Test]
    public async Task AddOrder_Should_Add_Order_To_InMemoryDb()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), new List<OrderItem>());

        // Act
        await _orderRepository.SaveAsync(order);
        var retrievedOrder = await _orderRepository.GetByIdAsync(order.Id);

        // Assert
        retrievedOrder.Should().NotBeNull();
        retrievedOrder.Id.Should().Be(order.Id);
    }

    [Test]
    public async Task GetById_Should_Return_Null_If_Order_Not_Found()
    {
        // Act
        var retrievedOrder = await _orderRepository.GetByIdAsync(Guid.NewGuid());

        // Assert
        retrievedOrder.Should().BeNull();
    }

    [Test]
    public async Task DeleteOrder_Should_Remove_Order_From_InMemoryDb()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), new List<OrderItem>());
        await _orderRepository.SaveAsync(order);

        // Act
        await _orderRepository.DeleteAsync(order.Id);
        var retrievedOrder = await _orderRepository.GetByIdAsync(order.Id);

        // Assert
        retrievedOrder.Should().BeNull(); // Bestellung sollte nach dem Löschen nicht mehr existieren
    }
}

using FluentAssertions;
using NUnit.Framework;
using OrderService.Domain.Aggregates;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure.Tests;

[TestFixture]
public class OrderRepositoryTests
{
    private AppDbContext _dbContext;
    private OrderRepository _orderRepository;

    [SetUp]
    public void SetUp()
    {
        _dbContext = AppDbContextFactory.CreateInMemoryContext();
        _orderRepository = new OrderRepository(_dbContext);
    }

    [TearDown]
    public void TearDown() => _dbContext.Dispose();

    [Test]
    public async Task AddOrder_Should_Add_Order_To_InMemoryDb()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), new OrderItem());

        // Act
        await _orderRepository.SaveAsync(order);
        Order retrievedOrder = await _orderRepository.GetByIdAsync(order.Id);

        // Assert
        retrievedOrder.Should().NotBeNull();
        retrievedOrder.Id.Should().Be(order.Id);
    }

    [Test]
    public async Task GetById_Should_Return_Null_If_Order_Not_Found()
    {
        // Act
        Order retrievedOrder = await _orderRepository.GetByIdAsync(Guid.NewGuid());

        // Assert
        retrievedOrder.Should().BeNull();
    }

    [Test]
    public async Task DeleteOrder_Should_Remove_Order_From_InMemoryDb()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), new OrderItem());
        await _orderRepository.SaveAsync(order);

        // Act
        await _orderRepository.DeleteAsync(order.Id);
        Order retrievedOrder = await _orderRepository.GetByIdAsync(order.Id);

        // Assert
        retrievedOrder.Should().BeNull();
    }
}

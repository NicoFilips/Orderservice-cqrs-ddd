using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Application.Exceptions;
using OrderService.Application.Repositories;
using OrderService.Domain.Aggregates;
using OrderService.Domain.Entities;

namespace Application.Tests;

[TestFixture]
public class CreateOrderCommandHandlerTests
{
    private Mock<IOrderRepository> _orderRepositoryMock;
    private Mock<IInventoryRepository> _inventoryRepositoryMock;
    private Mock<ILogger<CreateOrderCommandHandler>> _loggerMock;
    private CreateOrderCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _inventoryRepositoryMock = new Mock<IInventoryRepository>();
        _loggerMock = new Mock<ILogger<CreateOrderCommandHandler>>();

        _handler = new CreateOrderCommandHandler(
            _orderRepositoryMock.Object,
            _inventoryRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Test]
    public async Task Handle_Should_Create_Order_And_Return_Id()
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            CustomerId = Guid.NewGuid(),
            Item = new OrderItem
            {
                ProductId = Guid.NewGuid(),
                Quantity = 1,
            },
        };

        _inventoryRepositoryMock
           .Setup(repo => repo.ReserveItemsAsync(It.IsAny<KeyValuePair<Guid, int>>()))
           .Returns(Task.CompletedTask);

        _orderRepositoryMock
           .Setup(repo => repo.SaveAsync(It.IsAny<Order>()))
           .Returns(Task.CompletedTask);

        // Act
        Guid result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();

        _inventoryRepositoryMock.Verify(
            repo =>
                repo.ReserveItemsAsync(
                    It.Is<KeyValuePair<Guid, int>>(
                        q =>
                            q.Key == command.Item.ProductId && q.Value == command.Item.Quantity)),
            Times.Once);

        _orderRepositoryMock.Verify(
            repo =>
                repo.SaveAsync(It.IsAny<Order>()),
            Times.Once);
    }

    [Test]
    public async Task Handle_Should_Throw_DatabaseUnavailableException_If_Error_Occurs()
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            CustomerId = Guid.NewGuid(),
            Item = new OrderItem
            {
                ProductId = Guid.NewGuid(),
                Quantity = 1,
            },
        };

        _inventoryRepositoryMock
           .Setup(repo => repo.ReserveItemsAsync(It.IsAny<KeyValuePair<Guid, int>>()))
           .ThrowsAsync(new NUnitException("Database error"));

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DatabaseUnavailableException>()
                 .WithMessage("Failed to create order.");
    }
}

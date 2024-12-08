using FluentAssertions;
using NUnit.Framework;
using OrderService.Domain.Aggregates;
using OrderService.Domain.Entities;
using OrderService.Domain.Events;

namespace Domain.Tests;

[TestFixture]
public class OrderTests
{
    [Test]
    public void Cancel_ShouldSetStatusToCancelled_AddDomainEvent()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), new OrderItem(), "Created");

        // Act
        order.Cancel();

        // Assert
        order.Status.Should().Be("Cancelled");
        order.DomainEvents.Should().ContainSingle(e => e is OrderCancelled);
    }

    [Test]
    public void Cancel_ShouldThrowInvalidOperationException_IfStatusIsNotCreated()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), new OrderItem(), "Shipped");

        // Act
        Action act = () => order.Cancel();

        // Assert
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Only created orders can be cancelled.");
    }

    [Test]
    public void CancelShouldNotAddDomainEvent_IfExceptionThrown()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), new OrderItem(), "Delivered");

        // Act
        Action act = order.Cancel;

        // Assert
        act.Should().Throw<InvalidOperationException>();
        order.DomainEvents.Should().BeEmpty();
    }
}

using OrderService_cqrs_ddd.Application.Commands;
using OrderService_cqrs_ddd.Application.Domain.Events;
using OrderService_cqrs_ddd.Domain.Common;
using OrderService_cqrs_ddd.Domain.Entities;
using OrderService_cqrs_ddd.Domain.Events;

namespace OrderService_cqrs_ddd.Domain.Aggregates;

public class Order
{
    public Guid Id { get; private set; }
    public DateTime OrderDate { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public string Status { get; private set; }
    public decimal TotalAmount => Items.Sum(i => i.TotalPrice);

    public Order(List<OrderItem> items)
    {
        Id = Guid.NewGuid();
        OrderDate = DateTime.UtcNow;
        Items = items ?? throw new ArgumentNullException(nameof(items));
        Status = "Created";

        // Domain Event for order created
        AddDomainEvent(new OrderCreated(Id));
    }

    public void Cancel()
    {
        if (Status != "Created")
            throw new InvalidOperationException("Only created orders can be cancelled.");

        Status = "Cancelled";

        // Domain Event for order cancelled
        AddDomainEvent(new OrderCancelled(Id));
    }

    // Methode, um ein Domain Event hinzuzufügen
    private void AddDomainEvent(IDomainEvent domainEvent)
    {
        // Hier würdest du das Event in eine Liste von Events der Entität hinzufügen,
        // damit es später veröffentlicht werden kann.
        DomainEvents.Add(domainEvent);
    }
}

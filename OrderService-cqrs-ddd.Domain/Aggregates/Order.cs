using OrderService_cqrs_ddd.Domain.Common;
using OrderService_cqrs_ddd.Domain.Entities;
using OrderService_cqrs_ddd.Domain.Events;

namespace OrderService_cqrs_ddd.Domain.Aggregates;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }  // Neuer Parameter
    public DateTime OrderDate { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public string Status { get; private set; }
    public decimal TotalAmount => Items.Sum(i => i.TotalPrice);

    private List<IDomainEvent> _domainEvents;
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    public Order(Guid customerId, List<OrderItem> items)  // Konstruktor mit CustomerId
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;  // Weise CustomerId zu
        OrderDate = DateTime.UtcNow;
        Items = items ?? throw new ArgumentNullException(nameof(items));
        Status = "Created";

        // Domain Event for order created
        AddDomainEvent(new OrderCreated(Id));
    }

    // Parameterloser Konstruktor für EF Core
    protected Order()
    {
        // Dieser Konstruktor ist für EF Core gedacht und sollte nicht direkt verwendet werden
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
        _domainEvents = _domainEvents ?? new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }
}

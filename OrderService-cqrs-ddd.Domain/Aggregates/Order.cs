using OrderService_cqrs_ddd.Domain.Common;
using OrderService_cqrs_ddd.Domain.Entities;
using OrderService_cqrs_ddd.Domain.Events;

namespace OrderService_cqrs_ddd.Domain.Aggregates;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public string Status { get; private set; }
    public decimal TotalAmount => Items.Sum(i => i.TotalPrice);

    private List<IDomainEvent> _domainEvents;
    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    public Order(Guid customerId, List<OrderItem> items, List<IDomainEvent> domainEvents, string status)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        OrderDate = DateTime.UtcNow;
        Items = items ?? throw new ArgumentNullException(nameof(items));
        _domainEvents = domainEvents;
        Status = status;

        AddDomainEvent(new OrderCreated(Id));
    }

    public Order(List<IDomainEvent> domainEvents, List<OrderItem> items, string status)
    {
        _domainEvents = domainEvents;
        Items = items;
        Status = status;
    }

    public Order(Guid requestId, List<OrderItem> items)
    {
        Id = requestId;
        Items = items;
        _domainEvents = new List<IDomainEvent>();
        Status = "Created";
    }

    public void Cancel()
    {
        if (this.Status != "Created")
        {
            throw new InvalidOperationException("Only created orders can be cancelled.");
        }

        Status = "Cancelled";
        AddDomainEvent(new OrderCancelled(Id));
    }

    private void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }
}

using OrderService.Domain.Common;
using OrderService.Domain.Entities;
using OrderService.Domain.Events;

namespace OrderService.Domain.Aggregates;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; init; }

    private readonly OrderItem _item;
    public string Status { get; set; }

    private List<IDomainEvent> _domainEvents;
    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    public Order(Guid customerId, OrderItem item, List<IDomainEvent> domainEvents, string status)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        OrderDate = DateTime.UtcNow;
        _item = item ?? throw new ArgumentNullException(nameof(item));
        _domainEvents = domainEvents;
        Status = status;

        AddDomainEvent(new OrderCreated(Id));
    }

    public Order(List<IDomainEvent> domainEvents, OrderItem item, string status)
    {
        _domainEvents = domainEvents;
        _item = item;
        Status = status;
    }
    public Order(Guid orderId, OrderItem item, string status)
    {
        Id = orderId;
        _item = item;
        Status = status;
        _domainEvents = new List<IDomainEvent>();
    }

    public Order(Guid requestId, OrderItem item)
    {
        Id = requestId;
        _item = item;
        _domainEvents = new List<IDomainEvent>();
        Status = "Created";
    }

    public Order()
    {
        Id = Guid.Empty;
        CustomerId = Guid.Empty;
        OrderDate = DateTime.MinValue;
        _item = new OrderItem();
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

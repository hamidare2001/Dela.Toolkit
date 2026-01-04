namespace Dela.Toolkit.Domain;

public interface IAggregateRoot
{
    IReadOnlyList<IDomainEvent> UncommittedEvents { get; }
}

public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
{
    private readonly List<IDomainEvent> _uncommittedEvents = [];
    public IReadOnlyList<IDomainEvent> UncommittedEvents => _uncommittedEvents.AsReadOnly();

    public AggregateRoot(TKey id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }

    protected void Causes(IDomainEvent domainEvent)
    {
        _uncommittedEvents.Add(domainEvent);
    }

    protected void Publish<TEvent>(TEvent @event) where TEvent : DomainEvent
    {
        _uncommittedEvents.Add(@event);
    }

    public void ClearUncommittedEvents()
    {
        _uncommittedEvents.Clear();
    } 
}
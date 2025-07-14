using Dela.Toolkit.Domain.Events;

namespace Dela.Toolkit.Domain;

public abstract class AggregateRoot<TKey> : Entity<TKey>
{
    private readonly List<DomainEvent> _uncommittedEvents = [];

    protected AggregateRoot()
    {
    }

    protected void Causes(DomainEvent domainEvent)
    {
        _uncommittedEvents.Add(domainEvent);
    }
    public AggregateRoot(TKey id) : base(id)
    {
    }

    // protected void Publish<TEvent>(TEvent @event) where TEvent : DomainEvent
    // {
    //     this._uncommittedEvents.Add(@event);
    // }
    // public void ClearUncommittedEvents()
    // {
    //     this._uncommittedEvents.Clear();
    // }
    //public IReadOnlyList<DomainEvent> GetUncommittedEvents() => _uncommittedEvents.AsReadOnly();
}
namespace Dela.Toolkit.Domain;

public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime PublishDateTime { get; }
}

public abstract class DomainEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime PublishDateTime { get; } = DateTime.Now;

    protected DomainEvent()
    {
    }
}
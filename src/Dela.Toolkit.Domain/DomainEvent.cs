namespace Dela.Toolkit.Domain.Events;


public abstract class DomainEvent{

    public Guid EventId { get; private set; }

    public DateTime PublishDateTime { get; private set; }

    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
        PublishDateTime = DateTime.Now;
    }
}

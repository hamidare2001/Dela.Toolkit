namespace Dela.Toolkit.Application.Events;

public abstract class IntegrationEvent : IEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
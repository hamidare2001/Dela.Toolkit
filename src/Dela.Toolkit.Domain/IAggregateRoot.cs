namespace Dela.Toolkit.Domain;

public interface IAggregateRoot
{
    void ClearUncommittedEvents();
    //IReadOnlyList<DomainEvent> GetUncommittedEvents();
}
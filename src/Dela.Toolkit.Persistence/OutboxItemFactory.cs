using System.Text.Json;
using Dela.Toolkit.Domain;

namespace Dela.Toolkit.Persistence;

public static class OutboxItemFactory
{
    public static List<OutboxItem> CreateFromEventsOfAggregate(IAggregateRoot aggregate)
    {
        return aggregate.UncommittedEvents
            .Select(CreateOutboxItem)
            .ToList();
    }
   

    public static OutboxItem CreateOutboxItem(IDomainEvent domainEvent)
    {
        return new OutboxItem
        {
            EventId = domainEvent.EventId,
            PublishDateTime = domainEvent.PublishDateTime,
            EventType = domainEvent.GetType().Name,
            EventBody = JsonSerializer.Serialize(domainEvent)
        };
    }
}
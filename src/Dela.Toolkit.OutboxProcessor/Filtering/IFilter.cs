using Dela.Toolkit.Application.Events;

namespace Dela.Toolkit.OutboxProcessor.Filtering;

public interface IFilter
{
    bool ShouldPublish(IEvent @event);
}
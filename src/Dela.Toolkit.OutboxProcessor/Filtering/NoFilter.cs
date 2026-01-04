using Dela.Toolkit.Application.Events;

namespace Dela.Toolkit.OutboxProcessor.Filtering;

public class NoFilter : IFilter
{
    public bool ShouldPublish(IEvent @event)
    {
        return true;
    }
}
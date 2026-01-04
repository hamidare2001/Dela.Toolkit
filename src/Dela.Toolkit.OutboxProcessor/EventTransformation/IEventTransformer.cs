using Dela.Toolkit.Application.Events;

namespace Dela.Toolkit.OutboxProcessor.EventTransformation;

public interface IEventTransformer
{
    IEvent Transform(IEvent @event);

}
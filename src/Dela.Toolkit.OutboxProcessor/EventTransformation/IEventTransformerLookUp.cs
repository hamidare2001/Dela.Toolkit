using Dela.Toolkit.Application.Events;

namespace Dela.Toolkit.OutboxProcessor.EventTransformation;

public interface IEventTransformerLookUp
{
    IEventTransformer LookUpTransformer(IEvent @event);
}
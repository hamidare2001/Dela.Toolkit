using Dela.Toolkit.Application.Events;

namespace Dela.Toolkit.OutboxProcessor.EventBus;

public interface IEventBus
{
    Task Publish(IEvent @event);
    Task Start();
}
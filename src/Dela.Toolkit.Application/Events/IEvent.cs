namespace Dela.Toolkit.Application.Events;

public interface IEvent
{
}

public interface IEventHandler<T> where T : IEvent
{
    void Handle(T @event);
}
 

public interface IEventPublisher
{ 
    void Publish<T>(T eventToPublish) where T : IEvent;
}

public interface IEventListener
{ 
    ISubscription Subscribe<T>(IEventHandler<T> handler) where T : IEvent;
}
 

namespace Dela.Toolkit.Application.Events;

public class EventHandlerRegistrationService
{
    // private readonly IEventAggregator _eventBus;
    // private readonly IServiceProvider _serviceProvider;
    //
    // public EventHandlerRegistrationService(IEventAggregator eventBus, IServiceProvider serviceProvider)
    // {
    //     _eventBus = eventBus;
    //     _serviceProvider = serviceProvider;
    // }
    
    // public void RegisterEventHandlers(Assembly applicationAssembly)
    // {
    //     // Get all event handler types
    //     var handlerTypes = applicationAssembly
    //         .GetTypes()
    //         .Where(t => t is { IsClass: true, IsAbstract: false } && 
    //                     t.GetInterfaces().Any(i => 
    //                         i.IsGenericType && 
    //                         i.GetGenericTypeDefinition() == typeof(IEventHandler<>)));
    //     
    //     foreach (var handlerType in handlerTypes)
    //     {
    //         var eventType = handlerType.GetInterfaces()
    //             .First(i => i.IsGenericType && 
    //                         i.GetGenericTypeDefinition() == typeof(IEventHandler<>))
    //             .GetGenericArguments()[0];
    //     
    //         // Create generic method for subscription
    //         var subscribeMethod = typeof(EventHandlerRegistrationService)
    //             .GetMethod(nameof(SubscribeHandler), BindingFlags.NonPublic | BindingFlags.Instance)
    //             ?.MakeGenericMethod(eventType, handlerType);
    //     
    //         subscribeMethod?.Invoke(this, null);
    //     }
    // }
    
    // private void SubscribeHandler<TEvent, THandler>() 
    //     where TEvent : IEvent
    //     where THandler : IEventHandler<TEvent>
    // {
    //     _eventBus.Subscribe<TEvent>( @event =>
    //     {
    //         using var scope = _serviceProvider.CreateScope();
    //         var handler = scope.ServiceProvider.GetRequiredService<THandler>() as IEventHandler<TEvent>;
    //         //await handler.HandleAsync(@event);
    //          handler.Handle(@event);
    //     });
    // }
}
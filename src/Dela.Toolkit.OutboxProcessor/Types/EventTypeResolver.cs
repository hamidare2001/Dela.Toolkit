using System.Reflection;
using Dela.Toolkit.Application.Events;

namespace Dela.Toolkit.OutboxProcessor.Types;

public class EventTypeResolver : IEventTypeResolver
{
    private readonly Dictionary<string, Type> _types = new ();
    public void AddTypesFromAssembly(Assembly assembly)
    {
        var events = assembly.GetTypes().Where(type => typeof(IEvent).IsAssignableFrom(type)).ToList();
        events.ForEach(a =>
        {
            _types.Add(a.Name, a);
        });
    }

    public Type GetType(string typeName)
    {
        return _types.GetValueOrDefault(typeName);
    }
}
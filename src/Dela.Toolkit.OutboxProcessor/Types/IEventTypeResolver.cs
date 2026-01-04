using System.Reflection;

namespace Dela.Toolkit.OutboxProcessor.Types;

public interface IEventTypeResolver
{
    void AddTypesFromAssembly(Assembly assembly);
    Type GetType(string typeName);
}
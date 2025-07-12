using Autofac;
using Dela.Toolkit.Application;
using Microsoft.Extensions.DependencyInjection; 

namespace Dela.Toolkit.Autofac;

public static class DependencyInjection
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddTransient<ICommandDispatcher, CommandDispatcher>();
        services.AddTransient<IQueryDispatcher, QueryDispatcher>();
        return services;
    }

    public static IContainer AddApplicationWithAutofac(this Type type)
    {
        var builder = new ContainerBuilder();
        builder.RegisterAssemblyTypes(type.Assembly)
            .As(type => type
                .GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>))))
            // with scope lifetime
            .InstancePerLifetimeScope();    
         
        builder.RegisterDecorator(typeof(LoggingCommonHandlerDecorator<>), typeof(ICommandHandler<>));
        
        return builder.Build(); 
    }
}
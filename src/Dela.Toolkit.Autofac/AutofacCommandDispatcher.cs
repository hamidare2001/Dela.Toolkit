using Autofac;
using Dela.Toolkit.Application;
using Microsoft.Extensions.Logging; 

namespace Dela.Toolkit.Autofac;

public class AutofacCommandDispatcher: ICommandDispatcher
{
    private readonly ILogger<CommandDispatcher> _logger;
    //private readonly IServiceProvider _serviceProvider;
    private readonly ILifetimeScope _scope;

    public AutofacCommandDispatcher(ILifetimeScope scope, ILogger<CommandDispatcher> logger)
    {
        _scope = scope;
        _logger = logger;
    } 

    public async Task Dispatch<T>(T command, CancellationToken cancellationToken) where T : Application.ICommand
    {
        try
        {
            var handlers = _scope.Resolve<IEnumerable<ICommandHandler<T>>>();
            foreach (var commandHandler in handlers)
                await commandHandler.Handle(command, cancellationToken);
  
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, "Error in inject the service in Dispatch method in CommandDispatcher"); 
        }
    }

    // public Task<TCommandResult>? Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
    // {
    //     try
    //     {
    //         var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();
    //         return handler.Handle(command, cancellationToken);
    //     }
    //     catch (Exception e)
    //     { 
    //         _logger.LogError(e.Message,"Error in inject the service in Dispatch method in CommandDispatcher");
    //         return null;
    //     }
    // }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dela.Toolkit.Application;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider; 

    public CommandDispatcher(ILogger<CommandDispatcher> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
 

    public async Task Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.Handle(command, cancellationToken);
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
 
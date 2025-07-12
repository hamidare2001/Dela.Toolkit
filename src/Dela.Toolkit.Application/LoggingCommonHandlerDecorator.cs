using Microsoft.Extensions.Logging;

namespace Dela.Toolkit.Application;

public class LoggingCommonHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
{
    private readonly ICommandHandler<T> _commandHandler;
    private readonly ILogger _logger;


    public LoggingCommonHandlerDecorator(ILogger<LoggingCommonHandlerDecorator<T>> logger, ICommandHandler<T> commandHandler)
    {
        _logger = logger;
        _commandHandler = commandHandler;
    }

    public async Task Handle(T command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Command started ! {Name}", typeof(T).Name);
        await _commandHandler.Handle(command, cancellationToken);
        _logger.LogInformation("Command {Name} executed successfully", typeof(T).Name);
    }
}
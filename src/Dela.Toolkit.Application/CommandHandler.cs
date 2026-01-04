namespace Dela.Toolkit.Application;

public interface ICommand
{
}

public interface ICommandHandler<in TCommand, TCommandResult>  where TCommand : ICommand
{
    Task<TCommandResult>? Handle(TCommand command, CancellationToken cancellationToken);
}
public interface ICommandHandler<in TCommand>  where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandDispatcher //ICommandBus
{
    Task Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand;
}
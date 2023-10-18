namespace COLLM.CQRS.Interfaces;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Execute(TCommand command);
}

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> Execute(TCommand command);
}
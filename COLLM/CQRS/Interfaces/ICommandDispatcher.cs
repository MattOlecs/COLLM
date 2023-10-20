namespace COLLM.CQRS.Interfaces;

public interface ICommandDispatcher
{
    Task Dispatch<TCommand>(TCommand command) where TCommand : ICommand;
    Task<TResult> Dispatch<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
}
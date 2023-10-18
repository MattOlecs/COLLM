using COLLM.CQRS.Interfaces;

namespace COLLM.Interfaces.Services;

public interface ICommandDispatcher
{
    Task Dispatch<TCommand>(TCommand command) where TCommand : ICommand;
    Task<TResult> Dispatch<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
}
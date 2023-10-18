using COLLM.CQRS.Interfaces;

namespace COLLM.Interfaces.Services;

public interface IQueryDispatcher
{
    Task<TResult> Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
}
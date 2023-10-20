namespace COLLM.CQRS.Interfaces;

public interface IQueryDispatcher
{
    Task<TResult> Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
}
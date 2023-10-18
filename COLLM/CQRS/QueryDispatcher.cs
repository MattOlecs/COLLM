using COLLM.CQRS.Interfaces;
using COLLM.Interfaces.Services;

namespace COLLM.CQRS;

internal class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task<TResult> Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        var scope = _serviceProvider.CreateAsyncScope();
        var queryHandler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await queryHandler.Handle(query);
    }
}
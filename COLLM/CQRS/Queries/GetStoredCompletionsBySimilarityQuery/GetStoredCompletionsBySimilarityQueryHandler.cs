using COLLM.CQRS.Interfaces;
using COLLM.Interfaces.Services;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace COLLM.CQRS.Queries.GetStoredCompletionsBySimilarityQuery;

public class GetStoredCompletionsBySimilarityQueryHandler : IQueryHandler<GetStoredCompletionsBySimilarityQuery, Request[]>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IPythonScriptExecutor _pythonScriptExecutor;

    public GetStoredCompletionsBySimilarityQueryHandler(
        IRequestRepository requestRepository,
        IPythonScriptExecutor pythonScriptExecutor)
    {
        _requestRepository = requestRepository;
        _pythonScriptExecutor = pythonScriptExecutor;
    }
    
    public Task<Request[]> Handle(GetStoredCompletionsBySimilarityQuery query)
    {
        var requests = _requestRepository
            .GetBySimilarityAsync(r =>
                {
                    var firstCollection = r.Select(x => x.Question).ToArray();
                    var secondCollection = r.Select(x => query.Prompt).ToArray();
                    return _pythonScriptExecutor.GetSentencesSimilarityUsingSpacy(firstCollection, secondCollection);
                }, 
            query.Similarity);

        return requests;
    }
}
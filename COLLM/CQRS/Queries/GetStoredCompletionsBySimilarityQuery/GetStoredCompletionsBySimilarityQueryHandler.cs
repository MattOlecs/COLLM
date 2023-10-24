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
            .GetBySimilarity(r =>
                _pythonScriptExecutor.GetSentencesSimilarityUsingSpacy(query.Prompt, r.Question) > query.Similarity);

        return requests;
    }
}
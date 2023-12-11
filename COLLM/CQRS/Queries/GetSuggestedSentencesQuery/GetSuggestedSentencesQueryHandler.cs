using COLLM.CQRS.Interfaces;
using COLLM.DTO;
using COLLM.Interfaces.Services;
using DAL.Repositories.Interfaces;

namespace COLLM.CQRS.Queries.GetSuggestedSentencesQuery;

public class GetSuggestedSentencesQueryHandler : IQueryHandler<GetSuggestedSentencesQuery, SentenceDTO[]>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IPythonScriptExecutor _pythonScriptExecutor;

    public GetSuggestedSentencesQueryHandler(
        IRequestRepository requestRepository,
        IPythonScriptExecutor pythonScriptExecutor)
    {
        _requestRepository = requestRepository;
        _pythonScriptExecutor = pythonScriptExecutor;
    }
    
    public async Task<SentenceDTO[]> Handle(GetSuggestedSentencesQuery query)
    {
        var requests = await _requestRepository
            .GetWithHighestSimilarityAsync(r =>
                _pythonScriptExecutor.GetSentencesSimilarityUsingSpacy(query.GetSuggestedSentencesDto.Request,
                    r.Question), query.GetSuggestedSentencesDto.Length);

        return requests
            .Select(x => new SentenceDTO(x.Id, x.Text, x.Similarity))
            .ToArray();
    }
}
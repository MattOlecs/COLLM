using System.Diagnostics;
using COLLM.CQRS.Interfaces;
using COLLM.DTO;
using COLLM.Interfaces.Services;
using DAL.Records;
using DAL.Repositories.Interfaces;

namespace COLLM.CQRS.Queries.GetSuggestedSentencesQuery;

public class GetSuggestedSentencesQueryHandler : IQueryHandler<GetSuggestedSentencesQuery, SuggestionDTO[]>
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
    
    public async Task<SuggestionDTO[]> Handle(GetSuggestedSentencesQuery query)
    {
        var timer = new Stopwatch();
        timer.Start();

        var calculationTimer = new Stopwatch();
        Sentence[] requests = await _requestRepository
            .GetWithHighestSimilarityAsync(r =>
                _pythonScriptExecutor.GetSentencesSimilarityUsingSpacy(query.GetSuggestedSentencesDto.Request,
                    r.Question), query.GetSuggestedSentencesDto.Length);
        calculationTimer.Stop();
        Console.WriteLine($"calc time: {timer.Elapsed.Seconds} seconds");
        
        
        timer.Stop();
        Console.WriteLine($"Time: {timer.Elapsed.Seconds}");
        return requests
            .Select(x => new SuggestionDTO(x.Id, x.Text, x.Similarity))
            .OrderByDescending(x => x.Similarity)
            .ToArray();
    }
}
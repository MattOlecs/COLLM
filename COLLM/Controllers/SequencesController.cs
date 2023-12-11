using COLLM.CQRS.Interfaces;
using COLLM.CQRS.Queries.GetStoredCompletionsBySimilarityQuery;
using COLLM.CQRS.Queries.GetSuggestedSentencesQuery;
using COLLM.CQRS.Query.GetSentencesSimilarityQuery;
using COLLM.DTO;
using COLLM.Interfaces.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace COLLM.Controllers;

public class SequencesController : AbstractController
{
    private readonly IGptRequestCostSimulator _gptRequestCostSimulator;
    private readonly IQueryDispatcher _queryDispatcher;

    public SequencesController(
        IGptRequestCostSimulator gptRequestCostSimulator,
        IQueryDispatcher queryDispatcher)
    {
        _gptRequestCostSimulator = gptRequestCostSimulator;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpPost("similarity")]
    public async Task<double> GetSentencesSimilarity([FromBody] SentencesSimilarityDTO sentencesSimilarityDto)
    {
        return await _queryDispatcher.Dispatch<GetSentencesSimilarityQuery, double>(
            new GetSentencesSimilarityQuery(sentencesSimilarityDto));
    }
    
    [HttpPost("cost")]
    public double GetEstimatedCost([FromBody] string prompt)
    {
        return _gptRequestCostSimulator.GetPromptPrice(prompt);
    }
    
    [HttpPost("similar/suggestions")]
    public async Task<SentenceDTO[]> GetSuggestedSentences([FromBody] GetSuggestedSentencesDTO getSuggestedSentencesDto)
    {
        var result =
            await _queryDispatcher.Dispatch<GetSuggestedSentencesQuery, SentenceDTO[]>(
                new GetSuggestedSentencesQuery(getSuggestedSentencesDto));

        return result;
    }

    [HttpPost("similar")]
    public async Task<Request[]> GetRequestsBySimilarity([FromBody] GetSimilarSentencesDTO getSimilarSentencesDto)
    {
        var result = await _queryDispatcher.Dispatch<GetStoredCompletionsBySimilarityQuery, Request[]>(
            new GetStoredCompletionsBySimilarityQuery(getSimilarSentencesDto.Prompt,
                getSimilarSentencesDto.Similarity));

        return result;
    }
}
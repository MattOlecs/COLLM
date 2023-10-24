using COLLM.CQRS.Interfaces;
using COLLM.CQRS.Queries.GetStoredCompletionsBySimilarityQuery;
using COLLM.CQRS.Query.GetSentencesSimilarityQuery;
using COLLM.DTO;
using COLLM.Interfaces.Services;
using COLLM.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace COLLM.Controllers;

public class RequestsController : AbstractController
{
    private readonly IGptRequestCostSimulator _gptRequestCostSimulator;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ChatGptClient _chatGptClient;

    public RequestsController(
        IGptRequestCostSimulator gptRequestCostSimulator,
        IQueryDispatcher queryDispatcher,
        ChatGptClient chatGptClient)
    {
        _gptRequestCostSimulator = gptRequestCostSimulator;
        _queryDispatcher = queryDispatcher;
        _chatGptClient = chatGptClient;
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

    [HttpPost("similarity/request")]
    public async Task<Request[]> GetRequestsBySimilarity([FromBody] GetSimilarSentencesDTO getSimilarSentencesDto)
    {
        var result = await _queryDispatcher.Dispatch<GetStoredCompletionsBySimilarityQuery, Request[]>(
            new GetStoredCompletionsBySimilarityQuery(getSimilarSentencesDto.Prompt,
                getSimilarSentencesDto.Similarity));

        return result;
    }
}
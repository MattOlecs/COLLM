using COLLM.CQRS.Query;
using COLLM.DTO;
using COLLM.Interfaces.Services;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace COLLM.Controllers;

public class RequestsController : AbstractController
{
    private readonly IRequestRepository _requestRepository;
    private readonly IGptRequestCostSimulator _gptRequestCostSimulator;
    private readonly IPythonScriptExecutor _pythonScriptExecutor;
    private readonly IQueryDispatcher _queryDispatcher;

    public RequestsController(
        IRequestRepository requestRepository,
        IGptRequestCostSimulator gptRequestCostSimulator,
        IPythonScriptExecutor pythonScriptExecutor,
        IQueryDispatcher queryDispatcher)
    {
        _requestRepository = requestRepository;
        _gptRequestCostSimulator = gptRequestCostSimulator;
        _pythonScriptExecutor = pythonScriptExecutor;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IEnumerable<Request>> GetAll()
    {
        var requests = await _requestRepository.GetAllRequestsAsync();

        return requests;
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
}
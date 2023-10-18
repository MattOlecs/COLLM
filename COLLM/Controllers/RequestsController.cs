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

    public RequestsController(
        IRequestRepository requestRepository,
        IGptRequestCostSimulator gptRequestCostSimulator,
        IPythonScriptExecutor pythonScriptExecutor)
    {
        _requestRepository = requestRepository;
        _gptRequestCostSimulator = gptRequestCostSimulator;
        _pythonScriptExecutor = pythonScriptExecutor;
    }

    [HttpGet]
    public async Task<IEnumerable<Request>> GetAll()
    {
        var requests = await _requestRepository.GetAllRequestsAsync();

        return requests;
    }
    
    [HttpPost("similarity")]
    public double GetSentencesSimilarity([FromBody] SentencesSimilarityDTO sentencesSimilarityDto)
    {
        return _pythonScriptExecutor
            .GetSentencesSimilarityUsingSpacy(
                sentencesSimilarityDto.FirstSentence,
                sentencesSimilarityDto.SecondSentence);
    }

    [HttpPost("cost")]
    public double GetEstimatedCost([FromBody] string prompt)
    {
        return _gptRequestCostSimulator.GetPromptPrice(prompt);
    }
}
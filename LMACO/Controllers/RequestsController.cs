using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharpToken;

namespace LMACO.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestsController
{
    private readonly IRequestRepository _requestRepository;

    public RequestsController(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Request>> GetAll()
    {
        var requests = await _requestRepository.GetAllRequestsAsync();

        return requests;
    }
    
    [HttpPost("tokens/count")]
    public int CountTokens([FromBody] string sentence)
    {
        var encoding = GptEncoding.GetEncodingForModel("gpt-4");
        var encoded = encoding.Encode(sentence);

        return encoded.Count;
    }
}
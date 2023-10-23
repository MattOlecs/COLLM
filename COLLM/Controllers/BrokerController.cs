using COLLM.CQRS.Interfaces;
using COLLM.CQRS.Queries.GetChatGptResponseQuery;
using Microsoft.AspNetCore.Mvc;

namespace COLLM.Controllers;

public class BrokerController : AbstractController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public BrokerController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost("chat-gpt")]
    public async Task<string> GetChatGptResponse([FromBody] string prompt)
    {
        var response = await _queryDispatcher.Dispatch<GetChatGptResponseQuery, string>(new GetChatGptResponseQuery(prompt));
        return response;
    }
}
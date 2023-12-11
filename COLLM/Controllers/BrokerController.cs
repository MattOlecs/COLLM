using COLLM.CQRS.Interfaces;
using COLLM.CQRS.Queries.GetChatGptResponseQuery;
using COLLM.DTO;
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
    public async Task<ReadAIAnswerDTO> GetAIResponse([FromBody] GetAIResponseDTO getAiResponseDto)
    {
        var response =
            await _queryDispatcher.Dispatch<GetChatGptResponseQuery, ReadAIAnswerDTO>(
                new GetChatGptResponseQuery(getAiResponseDto.Prompt, getAiResponseDto.Similarity));
        return response;
    }
}
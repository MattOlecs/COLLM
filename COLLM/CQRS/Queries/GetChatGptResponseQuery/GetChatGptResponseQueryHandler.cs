using COLLM.CQRS.Interfaces;
using COLLM.DTO;
using COLLM.Interfaces.Services;
using COLLM.Services;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using OpenAI_API.Chat;

namespace COLLM.CQRS.Queries.GetChatGptResponseQuery;

public class GetChatGptResponseQueryHandler : IQueryHandler<GetChatGptResponseQuery, ReadAIAnswerDTO>
{
    private readonly ChatGptClient _chatGptClient;
    private readonly IConfiguration _configuration;
    private readonly IPythonScriptExecutor _pythonScriptExecutor;
    private readonly IRequestRepository _requestRepository;

    public GetChatGptResponseQueryHandler(
        ChatGptClient chatGptClient,
        IConfiguration configuration,
        IPythonScriptExecutor pythonScriptExecutor,
        IRequestRepository requestRepository)
    {
        _chatGptClient = chatGptClient;
        _configuration = configuration;
        _pythonScriptExecutor = pythonScriptExecutor;
        _requestRepository = requestRepository;
    }

    public async Task<ReadAIAnswerDTO> Handle(GetChatGptResponseQuery query)
    {
        var api = new OpenAI_API.OpenAIAPI(_configuration["ChatGptApiKey"]);
        var result = await api.Chat.CreateChatCompletionAsync(new ChatMessage(ChatMessageRole.User, query.Prompt));
        var answer = result.Choices[0].Message.Content;
        
        await _requestRepository.AddRequestAsync(
            new Request
            {
                Question = query.Prompt,
                Answer = answer
            });
        await _requestRepository.SaveChangesAsync();
        
        return new ReadAIAnswerDTO(answer, false);
    }
}
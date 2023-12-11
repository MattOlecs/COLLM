using COLLM.CQRS.Interfaces;
using COLLM.DTO;
using COLLM.Interfaces.Services;
using COLLM.Services;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using OpenAI_API.Completions;
using OpenAI_API.Models;

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
        var stored = await _requestRepository
            .GetBySimilarityAsync(r =>
                _pythonScriptExecutor.GetSentencesSimilarityUsingSpacy(query.Prompt, r.Question) > query.Similarity);

        if (stored.Length > 0)
        {
            return new ReadAIAnswerDTO(stored.First().Answer, true);
        }
        
        var api = new OpenAI_API.OpenAIAPI(_configuration["ChatGptApiKey"]);
        var result = await api.Completions.CreateCompletionAsync(new CompletionRequest(query.Prompt, Model.DavinciText, max_tokens: 1000));

        await _requestRepository.AddRequestAsync(
            new Request
            {
                Question = query.Prompt,
                Answer = result.Completions.First().Text
            });
        await _requestRepository.SaveChangesAsync();
        
        return new ReadAIAnswerDTO(result.Completions.First().Text, false);
    }
}
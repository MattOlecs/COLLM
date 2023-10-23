using COLLM.CQRS.Interfaces;
using COLLM.Services;
using OpenAI_API.Completions;
using OpenAI_API.Models;

namespace COLLM.CQRS.Queries.GetChatGptResponseQuery;

public class GetChatGptResponseQueryHandler : IQueryHandler<GetChatGptResponseQuery, string>
{
    private readonly ChatGptClient _chatGptClient;
    private readonly IConfiguration _configuration;
    
    public GetChatGptResponseQueryHandler(
        ChatGptClient chatGptClient,
        IConfiguration configuration)
    {
        _chatGptClient = chatGptClient;
        _configuration = configuration;
    }

    public async Task<string> Handle(GetChatGptResponseQuery query)
    {
        var api = new OpenAI_API.OpenAIAPI(_configuration["ChatGptApiKey"]);
        //var result = await api.Completions.GetCompletion(query.Prompt);
        var result = await api.Completions.CreateCompletionAsync(new CompletionRequest(query.Prompt, Model.DavinciText));
        return result.Completions.First().Text;
        // var response = await _chatGptClient.SendRequestAsync(query.Prompt);
        // return response;
    }
}
using System.Net.Http.Headers;
using COLLM.CQRS;
using COLLM.CQRS.Interfaces;
using COLLM.CQRS.Queries.GetChatGptResponseQuery;
using COLLM.CQRS.Queries.GetStoredCompletionsBySimilarityQuery;
using COLLM.CQRS.Queries.GetSuggestedSentencesQuery;
using COLLM.CQRS.Query.GetSentencesSimilarityQuery;
using COLLM.DTO;
using COLLM.Interfaces.Services;
using COLLM.Services;
using DAL.Entities;

namespace COLLM.Extensions;

internal static class ServicesExtensions
{
    internal static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .RegisterTransients()
            .RegisterSingletons();

        services.RegisterCQRS();
        
        services
            .AddHttpClient<ChatGptClient>((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri("https://api.openai.com");
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var apiKey = configuration["ChatGptApiKey"];
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            });

        return services;
    }

    private static IServiceCollection RegisterCQRS(this IServiceCollection services)
    {
        services
            .AddTransient<IQueryDispatcher, QueryDispatcher>()
            .AddTransient<ICommandDispatcher, CommandDispatcher>();

        services
            .AddTransient<IQueryHandler<GetSentencesSimilarityQuery, double>, GetSentencesSimilarityQueryHandler>()
            .AddTransient<IQueryHandler<GetChatGptResponseQuery, ReadAIAnswerDTO>, GetChatGptResponseQueryHandler>()
            .AddTransient<IQueryHandler<GetStoredCompletionsBySimilarityQuery, Request[]>, GetStoredCompletionsBySimilarityQueryHandler>()
            .AddTransient<IQueryHandler<GetSuggestedSentencesQuery, SentenceDTO[]>, GetSuggestedSentencesQueryHandler>();

        return services;
    } 

    private static IServiceCollection RegisterSingletons(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPythonScriptExecutor, PythonScriptExecutor>();
    }
    
    private static IServiceCollection RegisterTransients(this IServiceCollection services)
    {
        return services
            .AddTransient<IGptRequestCostSimulator, GptRequestCostSimulator>();
    }
}
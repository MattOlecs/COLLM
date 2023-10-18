using COLLM.CQRS;
using COLLM.CQRS.Interfaces;
using COLLM.CQRS.Query;
using COLLM.Interfaces.Services;
using COLLM.Services;

namespace COLLM.Extensions;

internal static class ServicesExtensions
{
    internal static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .RegisterTransients()
            .RegisterSingletons();

        services.RegisterCQRS();

        return services;
    }

    private static IServiceCollection RegisterCQRS(this IServiceCollection services)
    {
        services
            .AddTransient<IQueryDispatcher, QueryDispatcher>()
            .AddTransient<ICommandDispatcher, CommandDispatcher>();

        services
            .AddTransient<IQueryHandler<GetSentencesSimilarityQuery, double>, GetSentencesSimilarityQueryHandler>();

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
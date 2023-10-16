using LMACO.Interfaces.Services;
using LMACO.Services;

namespace LMACO.Extensions;

internal static class ServicesExtensions
{
    internal static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        return services
            .RegisterTransients()
            .RegisterSingletons();
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
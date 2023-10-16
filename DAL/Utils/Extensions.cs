using DAL.Contexts;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Utils;

public static class Extensions
{
    public static IServiceCollection RegisterLmacoDb(this IServiceCollection serviceCollection, ConfigurationManager config)
    {
        return serviceCollection
            .AddEntityFrameworkNpgsql().AddDbContext<LmacoContext>(
            options => options.UseNpgsql(config.GetConnectionString("PostgresDb"),  b => b.MigrationsAssembly("DAL")));
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTransient<IRequestRepository, RequestRepository>();
    }
}
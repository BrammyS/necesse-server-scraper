using Microsoft.Extensions.DependencyInjection;
using NecesseScraper.Persistence.MongoDB.Services;
using NecesseScraper.Persistence.MongoDB.Services.Implementation;

namespace NecesseScraper.Persistence.MongoDB;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        services.AddSingleton<IMongoConnectionService, MongoConnectionService>();
        
        services.AddSingleton<MongoContext>();
        
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using NecesseScraper.Persistence.MongoDB.Repositories;
using NecesseScraper.Persistence.MongoDB.Services;
using NecesseScraper.Persistence.MongoDB.Services.Implementation;
using NecesseScraper.Persistence.MongoDB.UnitOfWorks;
using NecesseScraper.Persistence.Repositories;
using NecesseScraper.Persistence.UnitOfWorks;

namespace NecesseScraper.Persistence.MongoDB;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        services.AddSingleton<IMongoConnectionService, MongoConnectionService>();
        
        services.AddSingleton<MongoContext>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<INecesseVersionRepository, NecesseVersionRepository>();
        
        return services;
    }
}
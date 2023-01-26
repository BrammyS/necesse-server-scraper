using Microsoft.Extensions.Configuration;
using NecesseScraper.Persistence.MongoDB.Models;

namespace NecesseScraper.Persistence.MongoDB.Services.Implementation;

public class MongoConnectionService : IMongoConnectionService
{
    private readonly IConfiguration _configuration;

    public MongoConnectionService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public MongoDbConnection GetConnection()
    {
        var section = _configuration.GetSection("Mongodb");
        return section.Get<MongoDbConnection>() ?? throw new NullReferenceException("Mongodb section is missing in the configuration file.");
    }

    public string GetConnectionString()
    {
        return GetConnection().GetConnectionString(true);
    }
}
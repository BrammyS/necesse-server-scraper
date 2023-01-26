using NecesseScraper.Persistence.MongoDB.Models;

namespace NecesseScraper.Persistence.MongoDB.Services;

public interface IMongoConnectionService
{
    MongoDbConnection GetConnection();
    string GetConnectionString();
}
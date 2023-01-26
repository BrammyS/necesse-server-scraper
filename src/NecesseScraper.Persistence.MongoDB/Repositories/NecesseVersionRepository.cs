using NecesseScraper.Persistence.Domain.Models;
using NecesseScraper.Persistence.Repositories;

namespace NecesseScraper.Persistence.MongoDB.Repositories;

/// <inheritdoc cref="INecesseVersionRepository" />
public class NecesseVersionRepository : Repository<NecesseVersion>, INecesseVersionRepository
{
    /// <summary>
    ///     Creates a new <see cref="NecesseVersionRepository" />.
    /// </summary>
    /// <param name="context">The <see cref="MongoContext" /> that will be used to query to the database.</param>
    public NecesseVersionRepository(MongoContext context) : base(context, nameof(NecesseVersion) + "s")
    {
    }
}
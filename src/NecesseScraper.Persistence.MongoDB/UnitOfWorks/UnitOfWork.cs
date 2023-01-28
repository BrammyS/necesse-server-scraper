using NecesseScraper.Persistence.Repositories;
using NecesseScraper.Persistence.UnitOfWorks;

namespace NecesseScraper.Persistence.MongoDB.UnitOfWorks;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(INecesseVersionRepository necesseVersions)
    {
        NecesseVersions = necesseVersions;
    }

    public INecesseVersionRepository NecesseVersions { get; }
}
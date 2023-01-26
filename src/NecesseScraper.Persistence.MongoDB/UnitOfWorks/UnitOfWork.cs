using NecesseScraper.Persistence.Repositories;
using NecesseScraper.Persistence.UnitOfWorks;

namespace NecesseScraper.Persistence.MongoDB.UnitOfWorks;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    public INecesseVersionRepository NecesseVersions { get; }

    public UnitOfWork(INecesseVersionRepository necesseVersions)
    {
        NecesseVersions = necesseVersions;
    }
}
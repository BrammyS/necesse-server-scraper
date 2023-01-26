using NecesseScraper.Persistence.Domain.Models;

namespace NecesseScraper.Services;

public interface IServerScraper
{
    Task<NecesseVersion> GetLatestVersionAsync();
}
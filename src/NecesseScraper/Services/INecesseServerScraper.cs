using NecesseScraper.Models;

namespace NecesseScraper.Services;

public interface INecesseServerScraper
{
    Task<NecesseVersion> GetLatestVersionAsync();
}
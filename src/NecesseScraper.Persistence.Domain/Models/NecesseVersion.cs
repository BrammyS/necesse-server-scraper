namespace NecesseScraper.Persistence.Domain.Models;

public record NecesseVersion(string Version, string Build, string Url, bool IsLatest) : BaseDocument;
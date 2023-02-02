using Microsoft.Extensions.Logging;
using NecesseScraper.Persistence.Domain.Models;
using NecesseScraper.Persistence.Repositories;

namespace NecesseScraper.Services.Implementation;

public class VersionUpdater : IVersionUpdater
{
    private readonly ILogger<VersionUpdater> _logger;
    private readonly IServerScraper _serverScraper;
    private readonly INecesseVersionRepository _versionRepository;

    public VersionUpdater(INecesseVersionRepository versionRepository, IServerScraper serverScraper, ILogger<VersionUpdater> logger)
    {
        _versionRepository = versionRepository;
        _serverScraper = serverScraper;
        _logger = logger;
    }

    public async Task UpdateVersionAsync()
    {
        var latestVersion = await _serverScraper.GetLatestVersionAsync().ConfigureAwait(false);
        var allVersions = await _versionRepository.GetAllAsync().ConfigureAwait(false);
        var currentVersion = allVersions.OrderByDescending(x => x.Build).ThenByDescending(x => x.AddedAtUtc).FirstOrDefault();

        if (currentVersion is not null && latestVersion.Build == currentVersion.Build)
        {
            _logger.LogInformation("No new version found");
            return;
        }

        await UpdateDatabaseAsync(latestVersion, currentVersion).ConfigureAwait(false);
        if (currentVersion is not null)
            await UpdateWorkflowFilesAsync(latestVersion, currentVersion).ConfigureAwait(false);
    }

    private async Task UpdateWorkflowFilesAsync(NecesseVersion latestVersion, NecesseVersion currentVersion)
    {
        var workFlow = await File.ReadAllTextAsync("publish_images.yml").ConfigureAwait(false);

        workFlow = workFlow.Replace(currentVersion.Url, latestVersion.Url);
        workFlow = workFlow.Replace(currentVersion.Version, latestVersion.Version);
        workFlow = workFlow.Replace(currentVersion.Build, latestVersion.Build);

        await File.WriteAllTextAsync("publish_images.yml", workFlow).ConfigureAwait(false);
    }

    private async Task UpdateDatabaseAsync(NecesseVersion latestVersion, NecesseVersion? currentVersion)
    {
        _logger.LogInformation("Adding new version to the database");
        await _versionRepository.AddAsync(latestVersion).ConfigureAwait(false);

        if (currentVersion is not null)
        {
            _logger.LogInformation("Setting current version as not latest");
            await _versionRepository.UpdateValueAsync(currentVersion.BsonObjectId, x => x.IsLatest, false).ConfigureAwait(false);
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NecesseScraper.Persistence.MongoDB;
using NecesseScraper.Services;
using NecesseScraper.Services.Implementation;

var configurationBuilder = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .SetBasePath(Path.Combine(AppContext.BaseDirectory))
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json", optional: true);

var serviceCollection = new ServiceCollection()
    .AddMongoDb()
    .AddSingleton<IConfiguration>(configurationBuilder.Build())
    .AddScoped<IVersionUpdater, VersionUpdater>()
    .AddScoped<IServerScraper, ServerScraper>()
    .AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.AddConsole();
    });

var services = serviceCollection.BuildServiceProvider();

var updater = services.GetRequiredService<IVersionUpdater>();
await updater.UpdateVersionAsync().ConfigureAwait(false);
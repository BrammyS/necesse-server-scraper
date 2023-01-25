// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NecesseScraper.Services;
using NecesseScraper.Services.Implementation;

var serviceCollection = new ServiceCollection()
    .AddScoped<INecesseServerScraper, NecesseServerScraper>()
    .AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.AddConsole();
    });

var services = serviceCollection.BuildServiceProvider();

var scraper = services.GetRequiredService<INecesseServerScraper>();
await scraper.GetLatestVersionAsync().ConfigureAwait(false);
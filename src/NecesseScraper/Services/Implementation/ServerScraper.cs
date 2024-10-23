using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NecesseScraper.Persistence.Domain.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NecesseScraper.Services.Implementation;

public class ServerScraper : IServerScraper
{
    private readonly ILogger<ServerScraper> _logger;

    public ServerScraper(ILogger<ServerScraper> logger)
    {
        _logger = logger;
    }

    public Task<NecesseVersion> GetLatestVersionAsync()
    {
        var webDriver = CreateChromeDriver();

        _logger.LogInformation("Srapping necessegame.com");
        var startTime = Stopwatch.GetTimestamp();
        webDriver.Navigate().GoToUrl("https://necessegame.com/server");
        _logger.LogInformation("Scraper finished after {Time:F1}ms", (Stopwatch.GetTimestamp() - startTime) / (double)Stopwatch.Frequency * 1000);

        var versionElement = webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div[4]/div/p[1]/a[3]"));
        _logger.LogInformation("Element found, parsing version");

        try
        {
            var necesseVersion = ParseVersionFromElement(versionElement);
            _logger.LogInformation("Version: {@Version} found", necesseVersion);

            if (!necesseVersion.Url.Contains(necesseVersion.Build)) throw new Exception("Incorrect download url, build number different in url");

            return Task.FromResult(necesseVersion);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to parse version");
            throw;
        }
    }

    private static NecesseVersion ParseVersionFromElement(IWebElement versionElement)
    {
        var versionData = versionElement.Text.Split(" ");
        var version = versionData[2].Replace('.', '-').Replace("v", "");
        var build = versionData[4];
        var downloadUrl = versionElement.GetAttribute("href");

        var necesseVersion = new NecesseVersion(version, build, downloadUrl, true);
        return necesseVersion;
    }

    private ChromeDriver CreateChromeDriver()
    {
        _logger.LogInformation("Starting chrome headless scraper");
        var chromeOptions = new ChromeOptions
        {
            AcceptInsecureCertificates = true
        };
        chromeOptions.AddArgument("--headless");
        chromeOptions.AddArgument("--no-sandbox");
        chromeOptions.AddArgument("--disable-dev-shm-usage");

        var webDriver = new ChromeDriver(chromeOptions);
        return webDriver;
    }
}
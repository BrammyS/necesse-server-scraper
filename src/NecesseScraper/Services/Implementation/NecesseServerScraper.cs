using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NecesseScraper.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NecesseScraper.Services.Implementation;

public class NecesseServerScraper : INecesseServerScraper
{
    private readonly ILogger<NecesseServerScraper> _logger;

    public NecesseServerScraper(ILogger<NecesseServerScraper> logger)
    {
        _logger = logger;
    }

    public async Task<NecesseVersion> GetLatestVersionAsync()
    {
        var webDriver = CreateChromeDriver();

        _logger.LogInformation("Srapping necessegame.com");
        var startTime = Stopwatch.GetTimestamp();
        webDriver.Navigate().GoToUrl("https://necessegame.com/server");
        _logger.LogInformation("Scraper finished after {Time:F1}ms", (Stopwatch.GetTimestamp() - startTime) / (double)Stopwatch.Frequency * 1000);

        var versionElement = webDriver.FindElement(By.XPath("/html/body/div/main/div/div/div/div/div[2]/div[2]/div[1]/div/p[6]/a"));
        _logger.LogInformation("Element found, parsing version");

        try
        {
            var necesseVersion = ParseVersionFromElement(versionElement);
            _logger.LogInformation("Version: {@Version} found", necesseVersion);
            return necesseVersion;
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
        var version = versionData[2];
        var build = versionData[4];
        var downloadUrl = versionElement.GetAttribute("href");

        var necesseVersion = new NecesseVersion(version, build, downloadUrl, true);
        return necesseVersion;
    }

    private ChromeDriver CreateChromeDriver()
    {
        _logger.LogInformation("Starting chrome headless scraper");
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--headless");
        chromeOptions.AddArgument("--no-sandbox");
        chromeOptions.AddArgument("--disable-dev-shm-usage");

        var webDriver = new ChromeDriver(chromeOptions);
        return webDriver;
    }
}
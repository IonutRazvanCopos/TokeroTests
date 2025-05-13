using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace TokeroTests;

public class PolicyTests
{
    private IBrowser _browser;
    private IBrowserContext _context;
    private IPage _page;

    public async Task Setup()
    {
        var playwright = await Playwright.CreateAsync();
        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();
    }

    public async Task TestPoliciesPageLoads()
    {
        await _page.GotoAsync("https://tokero.dev/en/");
        await _page.Locator("footer").ScrollIntoViewIfNeededAsync();
        await _page.ClickAsync("text=Policies");

        var links = await _page.Locator("a").AllAsync();
        Assert.IsTrue(links.Count > 0);
    }

    public async Task TestContactPageLoads()
    {
        await _page.GotoAsync("https://tokero.dev/en/contact");
        var contactHeader = await _page.Locator("h1, h2").First.OrNullAsync();
        Assert.IsNotNull(contactHeader);
    }

    public async Task TestPoliciesInRomanian()
    {
        await _page.GotoAsync("https://tokero.dev/ro/");
        await _page.Locator("footer").ScrollIntoViewIfNeededAsync();
        await _page.ClickAsync("text=Politici");
        var links = await _page.Locator("a").AllAsync();
        Assert.IsTrue(links.Count > 0);
    }

    public async Task TestPerformanceHomepage()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        await _page.GotoAsync("https://tokero.dev/en/");
        stopwatch.Stop();
        Assert.Less(stopwatch.ElapsedMilliseconds, 5000, "Page took too long to load");
    }

    public async Task Cleanup()
    {
        await _context.CloseAsync();
        await _browser.CloseAsync();
    }
}
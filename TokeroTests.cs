#nullable enable

using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace TokeroTests;

[TestFixture]
public class PolicyTests
{
    private IBrowser? _browser;
    private IBrowserContext? _context;
    private IPage? _page;
    private IPlaywright? _playwright;

    private async Task LaunchBrowser(string browserName)
    {
        _playwright = await Playwright.CreateAsync();

        _browser = browserName switch
        {
            "firefox" => await _playwright.Firefox.LaunchAsync(new() { Headless = true }),
            "webkit" => await _playwright.Webkit.LaunchAsync(new() { Headless = true }),
            _ => await _playwright.Chromium.LaunchAsync(new() { Headless = true }),
        };

        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();
    }

    [TestCase("chromium")]
    [TestCase("firefox")]
    [TestCase("webkit")]
    public async Task TestPoliciesPageLoads(string browserName)
    {
        await LaunchBrowser(browserName);
        await _page!.GotoAsync("https://tokero.dev/en/");
        await _page.Locator("footer").ScrollIntoViewIfNeededAsync();
        await AcceptCookiesIfPresent();
        await _page.ClickAsync("text=Policies");
        var links = await _page.Locator("a").AllAsync();
        Assert.IsTrue(links.Count > 0, "No links found on the policies page.");
    }

    [TestCase("chromium")]
    [TestCase("firefox")]
    [TestCase("webkit")]
    public async Task TestContactPageLoads(string browserName)
    {
        await LaunchBrowser(browserName);
        await _page!.GotoAsync("https://tokero.dev/en/contact");
        await AcceptCookiesIfPresent();

        var contactTitle = _page.Locator("h1", new() { HasTextString = "Contact us" });
        await contactTitle.WaitForAsync(new() { Timeout = 5000 });

        Assert.IsTrue(await contactTitle.IsVisibleAsync(), "Contact page did not load properly.");
    }

    [TestCase("chromium")]
    [TestCase("firefox")]
    [TestCase("webkit")]
    public async Task TestPoliciesInRomanian(string browserName)
    {
        await LaunchBrowser(browserName);
        await _page!.GotoAsync("https://tokero.dev/ro/");
        await _page.Locator("footer").ScrollIntoViewIfNeededAsync();
        await AcceptCookiesIfPresent();
        await _page.ClickAsync("text=Politici");
        var links = await _page.Locator("a").AllAsync();
        Assert.IsTrue(links.Count > 0, "No links found on Romanian policies page.");
    }

    [TestCase("chromium")]
    [TestCase("firefox")]
    [TestCase("webkit")]
    public async Task TestPerformanceHomepage(string browserName)
    {
        await LaunchBrowser(browserName);
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        await _page!.GotoAsync("https://tokero.dev/en/");
        stopwatch.Stop();
        Assert.Less(stopwatch.ElapsedMilliseconds, 5000, "Page load exceeded 5 seconds.");
    }

    private async Task AcceptCookiesIfPresent()
    {
        var cookieButton = _page!.Locator("button:has-text('Accept')");
        if (await cookieButton.IsVisibleAsync())
        {
            await cookieButton.ClickAsync();
        }
    }

    [TearDown]
    public async Task Cleanup()
    {
        if (_context is not null)
            await _context.CloseAsync();
        if (_browser is not null)
            await _browser.CloseAsync();
        _playwright?.Dispose();
    }
}

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

    [SetUp]
    public async Task Setup()
    {
        var playwright = await Playwright.CreateAsync();
        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();
    }

    [Test]
    public async Task TestPoliciesPageLoads()
    {
        await _page!.GotoAsync("https://tokero.dev/en/");
        await _page.Locator("footer").ScrollIntoViewIfNeededAsync();
        await AcceptCookiesIfPresent();
        await _page.ClickAsync("text=Policies");
        var links = await _page.Locator("a").AllAsync();
        Assert.IsTrue(links.Count > 0, "No links found on the policies page.");
    }

[Test]
public async Task TestContactPageLoads()
{
    await _page!.GotoAsync("https://tokero.dev/en/contact");
    await AcceptCookiesIfPresent();

    var contactTitle = _page.Locator("h1", new() { HasTextString = "Contact us" });
    await contactTitle.WaitForAsync(new() { Timeout = 5000 });

    Assert.IsTrue(await contactTitle.IsVisibleAsync(), "Contact page did not load properly.");
}

    [Test]
    public async Task TestPoliciesInRomanian()
    {
        await _page!.GotoAsync("https://tokero.dev/ro/");
        await _page.Locator("footer").ScrollIntoViewIfNeededAsync();
        await AcceptCookiesIfPresent();
        await _page.ClickAsync("text=Politici");
        var links = await _page.Locator("a").AllAsync();
        Assert.IsTrue(links.Count > 0, "No links found on Romanian policies page.");
    }

    [Test]
    public async Task TestPerformanceHomepage()
    {
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
    }
}
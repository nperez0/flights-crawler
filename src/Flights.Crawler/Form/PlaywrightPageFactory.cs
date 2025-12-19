using Microsoft.Playwright;

namespace Flights.Crawler.Form;

public class PlaywrightPageFactory : IDisposable, IPlaywrightPageFactory
{
    private IPlaywright? playwright;

    private bool disposed;

    public async Task<IPage> CreateAsync()
    {
        playwright ??= await Playwright.CreateAsync();

        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            Args =
            [
                "--disable-blink-features=AutomationControlled",
                "--disable-dev-shm-usage",
                "--no-sandbox"
            ]
        });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36",
            ViewportSize = new ViewportSize { Width = 1280, Height = 800 },
            Locale = "en-US",
            TimezoneId = "America/New_York",
            ExtraHTTPHeaders = new Dictionary<string, string>
            {
                { "Accept-Language", "en-US,en;q=0.9" }
            }
        });

        // Add script to remove webdriver property
        await context.AddInitScriptAsync(@"
            Object.defineProperty(navigator, 'webdriver', {
                get: () => undefined
            });
    
            // Override the permissions API
            const originalQuery = window.navigator.permissions.query;
            window.navigator.permissions.query = (parameters) => (
                parameters.name === 'notifications' ?
                    Promise.resolve({ state: Notification.permission }) :
                    originalQuery(parameters)
            );
    
            // Override plugins to make it look real
            Object.defineProperty(navigator, 'plugins', {
                get: () => [1, 2, 3, 4, 5]
            });
        ");

        var page = await context.NewPageAsync();

        await page.GotoAsync("https://matrix.itasoftware.com/search");

        return page;
    }


    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            playwright?.Dispose();
            disposed = true;
        }
    }
}

using Microsoft.Playwright;

namespace Flights.Crawler.Ita.Form;

public static class IPageExtensions
{
    public static Task TakeScreenshotAsync(this IPage page)
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var screenshotPath = $"flight-search_{timestamp}.png";

        return page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = screenshotPath,
            FullPage = true
        });
    }
}

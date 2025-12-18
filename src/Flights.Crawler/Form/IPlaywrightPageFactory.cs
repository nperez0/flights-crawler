using Microsoft.Playwright;

namespace Flights.Crawler.Form;

public interface IPlaywrightPageFactory
{
    Task<IPage> CreateAsync();
}
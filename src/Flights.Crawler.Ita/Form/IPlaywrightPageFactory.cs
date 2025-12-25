using Microsoft.Playwright;

namespace Flights.Crawler.Ita.Form;

public interface IPlaywrightPageFactory
{
    Task<IPage> CreateAsync();
}
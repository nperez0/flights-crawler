using Microsoft.Playwright;

namespace Flights.Crawler.Form;

public interface IQueryResponseInterceptorFactory
{
    Task<IQueryResponseInterceptor> CreateAsync(IPage page);
}
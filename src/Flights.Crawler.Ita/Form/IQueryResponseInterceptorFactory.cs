using Microsoft.Playwright;

namespace Flights.Crawler.Ita.Form;

public interface IQueryResponseInterceptorFactory
{
    Task<IQueryResponseInterceptor> CreateAsync(IPage page);
}
using Microsoft.Playwright;

namespace Flights.Crawler.Ita.Form;

public class QueryResponseInterceptorFactory : IQueryResponseInterceptorFactory
{
    public async Task<IQueryResponseInterceptor> CreateAsync(IPage page)
    {
        var interceptor = new QueryResponseInterceptor(page);

        await interceptor.InitializeAsync();

        return interceptor;
    }
}

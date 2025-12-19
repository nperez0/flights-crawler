using Flights.Crawler.Form;
using Flights.Crawler.Form.FormFillers;
using Flights.Data.Database.Repositories;
using Flights.Data.Mapping;
using Flights.Data.Models.Query;

namespace Flights.Crawler;

public class FlightsCrawler(
    IFlightQueryRepository flightQueryRepository,
    IFlightQueryResultRepository flightQueryResultRepository,
    IPlaywrightPageFactory playwrightPageFactory,
    IQueryResponseInterceptorFactory responseInterceptorFactory,
    IFormFillerFactory formFillerFactory) : IFlightsCrawler
{
    public async Task CrawlAsync()
    {
        var queries = await flightQueryRepository.GetEnabledQueriesAsync();

        foreach (var query in queries.Where(q => q.Type == FlightQueryType.RoundTrip))
            await ExecuteQuery(query);
    }

    private async Task ExecuteQuery(FlightQuery flightQuery)
    {
        var page = await playwrightPageFactory.CreateAsync();

        var responseInterceptor = await responseInterceptorFactory.CreateAsync(page);
        var formFiller = formFillerFactory.Create(page, flightQuery);

        await formFiller.FillFormAsync();
        await page.SubmitFormAsync();

        var queryResponse = await responseInterceptor.GetFlightQueryResponseAsync();
        var result = queryResponse.MapToResult(flightQuery.Id);

        await flightQueryResultRepository.SaveAsync(result);
        await page.CloseAsync();
    }
}

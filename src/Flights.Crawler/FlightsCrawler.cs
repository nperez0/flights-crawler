using Flights.Crawler.Database.Repositories;
using Flights.Crawler.Form;
using Flights.Crawler.Form.FormFillers;
using Flights.Crawler.Mapping;
using Flights.Crawler.Models.Queries;

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
        var flightQueries = await flightQueryRepository.GetAllAsync();

        foreach (var flightQuery in flightQueries)
            await SearchAsync(flightQuery);
    }

    private async Task SearchAsync(FlightQuery flightQuery)
    {
        var page = await playwrightPageFactory.CreateAsync();
        var responseInterceptor = await responseInterceptorFactory.CreateAsync(page);
        var formFiller = formFillerFactory.Create(page, flightQuery);

        await formFiller.FillFormAsync();
        await page.SubmitFormAsync();

        var queryResponse = await responseInterceptor.GetFlightQueryResponseAsync();
        var result = queryResponse.MapToResult(flightQuery);

        await flightQueryResultRepository.SaveAsync(result);
        await page.CloseAsync();
    }
}

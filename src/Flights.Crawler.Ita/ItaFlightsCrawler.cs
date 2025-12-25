using Flights.Crawler.Ita.Form;
using Flights.Crawler.Ita.Form.FormFillers;
using Flights.Data.Mapping;
using Flights.Data.Models.Query;
using Flights.Data.Models.Result;

namespace Flights.Crawler.Ita;

public class ItaFlightsCrawler(
    IPlaywrightPageFactory playwrightPageFactory,
    IQueryResponseInterceptorFactory responseInterceptorFactory,
    IFormFillerFactory formFillerFactory) 
    : IFlightsCrawler
{
    public async Task<FlightQueryResult[]> ExecuteQueriesAsync(FlightQuery[] flightQueries)
    {
        var results = new List<FlightQueryResult>();

        foreach (var flightQuery in flightQueries)
        {
            var resul = await ExecuteQuery(flightQuery);

            results.Add(resul);
        }

        return [.. results];
    }

    private async Task<FlightQueryResult> ExecuteQuery(FlightQuery flightQuery)
    {
        var page = await playwrightPageFactory.CreateAsync();

        var responseInterceptor = await responseInterceptorFactory.CreateAsync(page);
        var formFiller = formFillerFactory.Create(page, flightQuery);

        await formFiller.FillFormAsync();
        await page.SubmitFormAsync();

        var queryResponse = await responseInterceptor.GetFlightQueryResponseAsync();
        var result = queryResponse.MapToResult(flightQuery.Id);

        await page.CloseAsync();

        return result;
    }
}

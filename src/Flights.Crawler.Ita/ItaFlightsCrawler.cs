using Flights.Crawler.Ita.Form;
using Flights.Crawler.Ita.Form.FormFillers;
using Flights.Data.Models.Query;
using Flights.Data.Models.Result;
using Microsoft.Extensions.Logging;

namespace Flights.Crawler.Ita;

public class ItaFlightsCrawler(
    IPlaywrightPageFactory playwrightPageFactory,
    IQueryResponseInterceptorFactory responseInterceptorFactory,
    IFormFillerFactory formFillerFactory,
    ILogger<ItaFlightsCrawler> logger) 
    : IFlightsCrawler
{
    public string Name => "Ita Matrix Crawler";

    public async Task<FlightQueryResult[]> ExecuteQueriesAsync(FlightQuery[] flightQueries)
    {
        var results = new List<FlightQueryResult>();

        foreach (var flightQuery in flightQueries)
        {
            var result = await ExecuteQuery(flightQuery);

            results.Add(result);
        }

        return [.. results.Where(x => x != FlightQueryResult.Empty)];
    }

    private async Task<FlightQueryResult> ExecuteQuery(FlightQuery flightQuery)
    {
        try
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing flight query {FlightQueryId}", flightQuery.Id);
        }

        return FlightQueryResult.Empty;
    }
}

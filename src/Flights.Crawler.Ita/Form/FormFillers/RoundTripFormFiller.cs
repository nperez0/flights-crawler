using Flights.Data.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Ita.Form.FormFillers;

public class RoundTripFormFiller(IPage page, FlightQuery query) : IFormFiller
{
    public async Task FillFormAsync()
    {
        var flight = query.Segments[0];

        await page.SelectOriginAsync(flight.Origin.City, flight.Origin.Country);
        await page.SelectDestinationAsync(flight.Destination.City, flight.Destination.Country);
        await page.SelectDateRangeAsync(flight.Start, flight.End!.Value);

        await page.SelectDaysAsync(flight.Days);
        await page.SelectDaysAsync(flight.Days);

        await page.SelectStopsAsync(query.Stops);
        await page.SelectCurrencyAsync("EUR");
    }
}

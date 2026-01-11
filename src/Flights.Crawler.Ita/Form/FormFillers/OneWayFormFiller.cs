using Flights.Data.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Ita.Form.FormFillers;

public class OneWayFormFiller(IPage page, FlightQuery query) : IFormFiller
{
    public async Task FillFormAsync()
    {
        var flight = query.Segments[0];

        await page.SelectOneWaySearchAsync();

        await page.SelectOriginAsync(flight.Origin.City, flight.Origin.Country);
        await page.SelectDestinationAsync(flight.Destination.City, flight.Destination.Country);
        await page.SelectDateAsync(flight.Start);

        await page.SelectDaysAsync(flight.Days);

        await page.SelectCurrencyAsync("EUR");
    }
}

using Flights.Data.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Form.FormFillers;

public class RoundTripFormFiller(IPage page, FlightQuery query) : IFormFiller
{
    public async Task FillFormAsync()
    {
        var flight = query.Segments[0];

        await page.SelectOriginAsync(flight.Origin.City, flight.Origin.Country);
        await page.SelectDestinationAsync(flight.Destination.City, flight.Destination.Country);
        await page.SelectDateRange(flight.Start, flight.End!.Value);

        await page.SelectDaysAsync(flight.Days, FlightQueryDays.ThisDayOnly);
        await page.SelectDaysAsync(flight.Days, FlightQueryDays.ThisDayOnly);
    }
}

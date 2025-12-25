using Flights.Data.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Ita.Form.FormFillers;

public class MultiCityFormFiller(IPage page, FlightQuery query) : IFormFiller
{
    public async Task FillFormAsync()
    {
        await page.SelectMultiCitySearchAsync();

        for (int i = 0; i < query.Segments.Length; i++)
        {
            var flight = query.Segments[i];

            await page.SelectOriginAsync(flight.Origin.City, flight.Origin.Country, i);
            await page.SelectDestinationAsync(flight.Destination.City, flight.Destination.Country, i);
            await page.SelectDateAsync(flight.Start, i);

            await page.SelectDaysAsync(flight.Days, i == 0 ? FlightQueryDays.ThisDayOnly : query.Segments[i - 1].Days, i);

            if (i < query.Segments.Length - 1)
                await page.AddFlightAsync(i);
        }
    }
}

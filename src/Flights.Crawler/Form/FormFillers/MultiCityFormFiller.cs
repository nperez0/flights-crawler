using Flights.Crawler.Models.Queries;
using Flights.Crawler.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Form.FormFillers;

public class MultiCityFormFiller(IPage page, FlightQuery search) : IFormFiller
{
    public async Task FillFormAsync()
    {
        await page.SelectMultiCitySearchAsync();

        for (int i = 0; i < search.Segments.Length; i++)
        {
            var flight = search.Segments[i];

            await page.SelectOriginAsync(flight.Origin.City, flight.Origin.Country, i);
            await page.SelectDestinationAsync(flight.Destination.City, flight.Destination.Country, i);
            await page.SelectDate(flight.Date.ToString("MM/dd/yyyy"), i);

            await page.SelectDaysAsync(flight.Days, i == 0 ? FlightQueryDays.ThisDayOnly : search.Segments[i - 1].Days, i);

            if (i < search.Segments.Length - 1)
                await page.AddFlightAsync(i);
        }
    }
}

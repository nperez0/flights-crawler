using Flights.Data.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Ita.Form.FormFillers;

public class OneWayFormFiller(IPage page, FlightQuery query) : IFormFiller
{
    public Task FillFormAsync()
    {
        throw new NotImplementedException();
    }
}

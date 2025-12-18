using Flights.Data.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Form.FormFillers;

public class OneWayFormFiller(IPage page, FlightQuery search) : IFormFiller
{
    public Task FillFormAsync()
    {
        throw new NotImplementedException();
    }
}

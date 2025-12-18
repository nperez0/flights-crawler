using Flights.Data.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Form.FormFillers;

public class RoundTripFormFiller(IPage page, FlightQuery search) : IFormFiller
{
    public Task FillFormAsync()
    {
        throw new NotImplementedException();
    }
}

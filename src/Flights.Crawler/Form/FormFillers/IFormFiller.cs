using Flights.Crawler.Models;

namespace Flights.Crawler.Form.FormFillers;

public interface IFormFiller
{
    Task FillFormAsync();
}

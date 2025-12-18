using Flights.Crawler.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Form;

public static class FormActions
{
    public static async Task SelectMultiCitySearchAsync(this IPage page)
    {
        var selMultiCityButton = page.GetByRole(AriaRole.Tab, new() { Name = "Multi City" });

        await selMultiCityButton.ClickAsync();
    }

    public static async Task SelectOriginAsync(this IPage page, string city, string country, int position)
    {
        var selOriginFlight = page.Locator("matrix-location-field[placeholder='Origin'] input");
        var selOriginCity = page.Locator($"mat-option:has-text('{city}, {country}')");

        await selOriginFlight
            .Select(position)
            .FillAsync(city);

        await selOriginCity.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await selOriginCity.ClickAsync();
    }

    public static async Task SelectDestinationAsync(this IPage page, string city, string country, int position)
    {
        var selDestinationFlight = page.Locator("matrix-location-field[placeholder='Destination'] input");
        var selDestinationCity = page.Locator($"mat-option:has-text('{city}, {country}')");

        await selDestinationFlight
            .Select(position)
            .FillAsync(city);

        await selDestinationCity.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await selDestinationCity.ClickAsync();
    }

    public static async Task SelectDate(this IPage page, string date, int position)
    {
        var selDateFlight = page.Locator("input[matinput][required].mat-datepicker-input");

        await selDateFlight
            .Select(position)
            .FillAsync(date);
    }

    public static async Task SelectDaysAsync(this IPage page, FlightQueryDays searchDays, FlightQueryDays previousSearchDays, int position)
    {
        var selDays = page.GetByRole(AriaRole.Combobox, new() { Name = GetOptionName(previousSearchDays) });
        var selDaysOption = page.Locator($"mat-option:has-text('{GetOptionName(searchDays)}')");

        await selDays.Select(position).ClickAsync();

        await selDaysOption.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await selDaysOption.ClickAsync();
    }

    public static async Task AddFlightAsync(this IPage page, int currentSegment)
    {
        var selAddFlightButton = page.GetByRole(AriaRole.Button, new() { Name = "Add Flight" });

        await selAddFlightButton.ClickAsync();

        var selRemoveCity = page.Locator($"matrix-location-field[placeholder='Origin'] mat-chip-row mat-icon[matchipremove]");

        await selRemoveCity.Select(currentSegment + 1).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await selRemoveCity.Select(currentSegment + 1).ClickAsync();
    }

    public static Task SubmitFormAsync(this IPage page) 
        => page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

    private static ILocator Select(this ILocator locator, int position)
    {
        var all = locator.AllAsync().GetAwaiter().GetResult();

        return all
            .Skip(position)
            .First();
    }

    private static string GetOptionName(FlightQueryDays searchDays) => searchDays switch
    {
        FlightQueryDays.ThisDayOnly => "This day only",
        FlightQueryDays.DayBefore => "Or day before",
        FlightQueryDays.DayAfter => "Or day after",
        FlightQueryDays.PlusMinusOneDay => "+/- 1 day",
        FlightQueryDays.PlusMinusTwoDays => "+/- 2 days",
        _ => throw new ArgumentOutOfRangeException(nameof(searchDays), searchDays, null)
    };
}

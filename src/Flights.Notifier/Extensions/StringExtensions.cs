namespace Flights.Notifier.Extensions;

public static class StringExtensions
{
    public static decimal ParsePrice(this string price)
    {
        if (decimal.TryParse(price, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var value))
            return value;

        var cleaned = new string(price.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
        cleaned = cleaned.Replace(',', '.');

        return decimal.TryParse(cleaned, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out value)
            ? value
            : 0m;
    }
}

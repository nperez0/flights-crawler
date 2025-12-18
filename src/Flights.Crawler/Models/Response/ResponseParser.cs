using System.Text.Json;

namespace Flights.Crawler.Models.Response;

public static class ResponseParser
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public static FlightQueryResponse ParseResponse(string responseText)
    {
        var jsonData = ExtractJsonFromMultipartResponse(responseText);

        if (jsonData == null)
            return FlightQueryResponse.Empty;

        return JsonSerializer.Deserialize<FlightQueryResponse>(jsonData, JsonOptions) ?? FlightQueryResponse.Empty;
    }

    private static string? ExtractJsonFromMultipartResponse(string responseText)
    {
        // Find the start of the JSON (after the HTTP headers in the multipart body)
        var jsonStartIndex = responseText.IndexOf("\r\n\r\n{", StringComparison.Ordinal);
        if (jsonStartIndex == -1)
        {
            // Try with just \n\n for different line ending formats
            jsonStartIndex = responseText.IndexOf("\n\n{", StringComparison.Ordinal);
            if (jsonStartIndex == -1)
            {
                return null;
            }
            jsonStartIndex += 2; // Skip past \n\n
        }
        else
        {
            jsonStartIndex += 4; // Skip past \r\n\r\n
        }

        // Find the end of the JSON (before the closing boundary)
        var jsonEndIndex = responseText.IndexOf("\n--batch_", jsonStartIndex, StringComparison.Ordinal);
        if (jsonEndIndex == -1)
        {
            jsonEndIndex = responseText.IndexOf("\r\n--batch_", jsonStartIndex, StringComparison.Ordinal);
            if (jsonEndIndex == -1)
            {
                // If no boundary found, assume JSON goes to the end
                jsonEndIndex = responseText.Length;
            }
        }

        // Extract the JSON string
        return responseText.Substring(jsonStartIndex, jsonEndIndex - jsonStartIndex).Trim();
    }
}

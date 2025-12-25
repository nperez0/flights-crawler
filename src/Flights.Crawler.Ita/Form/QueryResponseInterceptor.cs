using Flights.Crawler.Ita.Response;
using Microsoft.Playwright;

namespace Flights.Crawler.Ita.Form;

public class QueryResponseInterceptor(IPage page) : IQueryResponseInterceptor
{
    private Task<IResponse>? responseTask;

    public async Task InitializeAsync()
    {
        responseTask = page.WaitForResponseAsync(response =>
            response.Url.Contains("content-alkalimatrix-pa.googleapis.com/batch") &&
            response.Request.Method == "POST" &&
            response.Request.PostData!.Contains("\"summarizers\"") &&
            response.Status == 200,
            new() { Timeout = 60000 });
    }

    public Task<FlightQueryResponse> GetFlightQueryResponseAsync()
    {
        if (responseTask == null)
            return Task.FromResult(FlightQueryResponse.Empty);

        return GetResponseInternalAsync();
    }

    private async Task<FlightQueryResponse> GetResponseInternalAsync()
    {
        var response = await responseTask!;
        var responseBodyText = await response.TextAsync();

        return ResponseParser.ParseResponse(responseBodyText);
    }
}

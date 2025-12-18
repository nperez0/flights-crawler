using System.Text.Json.Serialization;

namespace Flights.Data.Models.Response;

public class SolutionList
{
    [JsonPropertyName("pages")]
    public PageInfo? Pages { get; set; }

    [JsonPropertyName("solutions")]
    public List<Solution>? Solutions { get; set; }

    [JsonPropertyName("minPrice")]
    public string? MinPrice { get; set; }

    [JsonPropertyName("solutionCount")]
    public int SolutionCount { get; set; }
}

public class PageInfo
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("current")]
    public int Current { get; set; }
}

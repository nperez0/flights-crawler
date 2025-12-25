using System.Text.Json.Serialization;

namespace Flights.Crawler.Ita.Response;

public class CarrierStopMatrix
{
    [JsonPropertyName("columns")]
    public List<MatrixColumn>? Columns { get; set; }

    [JsonPropertyName("rows")]
    public List<MatrixRow>? Rows { get; set; }
}

public class MatrixColumn
{
    [JsonPropertyName("label")]
    public Carrier? Label { get; set; }
}

public class MatrixRow
{
    [JsonPropertyName("cells")]
    public List<MatrixCell>? Cells { get; set; }

    [JsonPropertyName("label")]
    public int Label { get; set; }
}

public class MatrixCell
{
    [JsonPropertyName("minPrice")]
    public string? MinPrice { get; set; }

    [JsonPropertyName("minPriceInRow")]
    public bool? MinPriceInRow { get; set; }

    [JsonPropertyName("minPriceInColumn")]
    public bool? MinPriceInColumn { get; set; }

    [JsonPropertyName("minPriceInGrid")]
    public bool? MinPriceInGrid { get; set; }
}

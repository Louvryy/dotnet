using System.Text.Json.Serialization;

namespace Louvryy.Core.Tests.MinimalApi.Responses;

public record PaginationResponse<T>
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("items")]
    public IEnumerable<T> Items { get; set; }

    [JsonPropertyName("lastPage")]
    public int LastPage { get; set; }
}
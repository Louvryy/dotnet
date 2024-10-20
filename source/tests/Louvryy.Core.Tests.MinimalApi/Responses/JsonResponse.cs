using System.Text.Json.Serialization;
using Microsoft.AspNetCore.WebUtilities;

namespace Louvryy.Core.Tests.MinimalApi.Responses;

public class JsonResponseErrors : Dictionary<string, string[]>
{ }

/// <summary>
/// Constructor method
/// </summary>
/// <param name="statusCode">Response status code</param>
/// <param name="message">Response message</param>
public class JsonResponse<TData>(int statusCode = 200, string message = null)
{
    /// <summary>
    /// Response message
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = message ?? ReasonPhrases.GetReasonPhrase(StatusCodes.Status200OK);

    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// Response status code
    /// </summary>
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; } = statusCode;

    /// <summary>
    /// Response data
    /// </summary>
    [JsonPropertyName("data")]
    public TData Data { get; set; }

    /// <summary>
    /// Response validation errors
    /// </summary>
    [JsonPropertyName("errors")]
    public JsonResponseErrors Errors { get; set; }
}


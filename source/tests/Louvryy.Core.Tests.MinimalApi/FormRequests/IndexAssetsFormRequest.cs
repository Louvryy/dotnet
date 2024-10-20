namespace Louvryy.Core.Tests.MinimalApi.FormRequests;

public record IndexAssetsFormRequest(string? Search, int? Page, bool? OrderByCrescent);
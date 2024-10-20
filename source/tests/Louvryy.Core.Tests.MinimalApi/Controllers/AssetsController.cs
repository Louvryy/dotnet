using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Louvryy.Core.Domain.UseCases;
using Louvryy.Core.Tests.MinimalApi.FormRequests;
using Louvryy.Core.Tests.MinimalApi.Responses;

namespace Louvryy.Api.Web.Controllers;

/// <summary>
/// AssetsController
/// </summary>
[Route("/assets")]
public class AssetsController : Controller {

    /// <summary>
    /// Original assets pagination
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JsonResponse<PaginationResponse<AssetResponse>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(JsonResponse<object>))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(JsonResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(JsonResponse<object>))]
    public async Task<JsonResponse<PaginationResponse<AssetResponse>>> Index(
        [FromQuery] IndexAssetsFormRequest query,
        [FromServices] IMapper mapper,
        [FromServices] GetPaginatedOriginalAssetsUseCase useCase
    ) {

        var result = await useCase.Execute(new GetPaginatedOriginalAssetsUseCaseInput {
            Page = query.Page ?? 1,
            Search = query.Search,
            OrderByCrescent = query.OrderByCrescent,
        });

        return new JsonResponse<PaginationResponse<AssetResponse>>() {
            Data = mapper.Map<PaginationResponse<AssetResponse>>(result)
        };
    }
}
using AutoMapper;
using Louvryy.Core.DTOs;
using Louvryy.Core.Domain.DTOs;
using Louvryy.Core.Domain.Interfaces.Core;
using Louvryy.Core.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Louvryy.Core.Domain.UseCases;

public record GetPaginatedOriginalAssetsUseCaseInput
{
    public required int Page { get; set; }
    public string? Search { get; set; }
    public bool? OrderByCrescent { get; set; }
}

public class GetPaginatedOriginalAssetsUseCase(
    [FromKeyedServices("LouvryyMapper")] IMapper mapper,
    IAssetRepository assetRepository
    ) : IUseCase<GetPaginatedOriginalAssetsUseCaseInput, PaginationDTO<AssetDTO>>
{
    private readonly IAssetRepository AssetRepository = assetRepository;
    private readonly IMapper Mapper = mapper;

    public Task<PaginationDTO<AssetDTO>> Execute(GetPaginatedOriginalAssetsUseCaseInput input)
    {
        var pagination = AssetRepository
                .PaginateOriginals(12, input.Page, input.Search, input.OrderByCrescent);

        var mappedPagination = Mapper.Map<PaginationDTO<AssetDTO>>(pagination);

        return Task.FromResult(mappedPagination);
    }
}
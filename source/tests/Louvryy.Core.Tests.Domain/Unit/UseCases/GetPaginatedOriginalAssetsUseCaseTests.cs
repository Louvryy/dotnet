using Moq;
using AutoMapper;
using Louvryy.Core.DTOs;
using Louvryy.Core.Data.Models;
using Louvryy.Core.Domain.DTOs;
using Louvryy.Core.Domain.Interfaces.Repositories;
using Louvryy.Core.Domain.UseCases;
using Louvryy.Core.Tests.Domain.Fixtures;
using Louvryy.Core.Tests.Fixtures.EntityBuilders;
using Louvryy.Core.Data.Utils;

namespace Louvryy.Core.Tests.Domain.Unit.UseCases;

[Collection("Tests.Domain.Application")]
public class GetPaginatedOriginalAssetsUseCaseTests(ApplicationFixture application)
{
    private ApplicationFixture Application { get; } = application;

    [Fact]
    public async void GetPaginatedOriginalAssets_When_Called_Should_Returns_PaginationDto()
    {
        //Assert
        var page = 1;
        var pageSize = 12;
        string? search = null;
        bool? orderBy = null;

        var assetRepositoryMock = new Mock<IAssetRepository>();
        var mapperMock = new Mock<IMapper>();

        var assets = AssetBuilder
            .Setup()
            .BuildMany(pageSize);

        var pagination = new Pagination<Asset>(page, pageSize, 12, assets);

        assetRepositoryMock
            .Setup(opt => opt.PaginateOriginals(pageSize, page, search, orderBy))
            .Returns(pagination);

        mapperMock
            .Setup(mapper => mapper.Map<PaginationDTO<AssetDTO>>(pagination))
            .Returns((Pagination<Asset> pagedObject) => {
                return new PaginationDTO<AssetDTO>() {
                    LastPage = pagedObject.LastPage,
                    Page = pagedObject.Page,
                    PageSize = pagedObject.PageSize,
                    Total = pagedObject.Total,
                    Items = pagedObject.Items.Select(a => new AssetDTO {
                        Id = a.Id
                    })
                };
            })
            .Verifiable(Times.Once());

        var useCase = new GetPaginatedOriginalAssetsUseCase(mapperMock.Object, assetRepositoryMock.Object);

        // Act
        var result = await useCase.Execute(new GetPaginatedOriginalAssetsUseCaseInput
        {
            Page = page,
            Search = search,
            OrderByCrescent = orderBy
        });

        // Assert
        Assert.IsAssignableFrom<PaginationDTO<AssetDTO>>(result);
        Assert.Equal(pageSize, result.Items.Count());
        Assert.Equal(page, result.Page);
    }
}
using Louvryy.Core.DTOs;
using Louvryy.Core.Domain.DTOs;
using Louvryy.Core.Domain.UseCases;
using Louvryy.Core.Tests.Domain.Fixtures;
using Louvryy.Core.Tests.Fixtures.EntityBuilders;

namespace Louvryy.Core.Tests.Domain.Integration.UseCases;

[Collection("Tests.Domain.Application")]
public class GetPaginatedOriginalAssetsUseCaseIntegrationTests(ApplicationFixture application)
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

        AssetBuilder
            .Setup()
            .Materialize(Application.DbContextFixture)
            .BuildMany(12);

        var useCase = Application.GetService<GetPaginatedOriginalAssetsUseCase>();

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
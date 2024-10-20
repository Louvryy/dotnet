using Louvryy.Core.Data.Utils;
using Louvryy.Core.Data.Models;
using Louvryy.Core.Data.Repositories;
using Louvryy.Core.Tests.Data.Fixtures;
using Louvryy.Core.Tests.Fixtures.DbContextFixture;
using Louvryy.Core.Tests.Fixtures.EntityBuilders;

namespace Louvryy.Core.Tests.Data;

[Collection("Tests.Data.Application")]
public class AssetRepositoryTests
{
    private AssetRepository<TestDbContext> Repository { get; }
    private ApplicationFixture Application { get; }

    private const int AssetesCount = 20;
    private IEnumerable<Asset> Assets { get; set; } = new List<Asset>();

    public AssetRepositoryTests(ApplicationFixture application) {
        Application = application;
        Repository = new(application.DbContextFixture.DbContext);
    }

    #region Paginate originals

    [Fact(DisplayName = "Paginate only original assets")]
    public async Task PaginateOriginals_When_Called_Returns_Pagination_With_Only_Original_Assets()
    {
        // Arrange
        await SetupData();

        var perPage = 10;
        var page = 1;

        // Act
        var result = Repository.PaginateOriginals(perPage, page);

        // Assert
        Assert.IsAssignableFrom<Pagination<Asset>>(result);
        Assert.Equal(perPage, result.Items.Count());
        Assert.Equal(AssetesCount, result.Total);
        Assert.Equal(page, result.Page);
    }

    [Fact(DisplayName = "Paginate only original assets by search terms")]
    public async Task PaginateOriginals_When_Called_With_Search_Returns_Pagination_With_Only_Original_Assets()
    {
        // Arrange
        await SetupData();

        var perPage = 10;
        var page = 1;

        var firstItem = Assets.First();

        // Act
        var result = Repository.PaginateOriginals(perPage, page, firstItem.Name);

        // Assert
        Assert.IsAssignableFrom<Pagination<Asset>>(result);
        Assert.Equal(firstItem.Id, result.Items.First().Id);
        Assert.Single(result.Items);
    }

    [Theory(DisplayName = "Paginate only original assets by order criteria")]
    [InlineData(false)]
    [InlineData(true)]
    public async void PaginateOriginals_When_Called_With_OrderBy_Returns_Pagination_With_Only_Original_Assets(bool orderByCrescent)
    {
        // Arrange
        await SetupData();

        var perPage = 10;
        var page = 1;

        var firstItem = Assets.First();

        // Act
        var result = Repository.PaginateOriginals(perPage, page, orderByCrescent: orderByCrescent);

        // Assert
        Assert.IsAssignableFrom<Pagination<Asset>>(result);

        var itensAsArray = result.Items.ToArray();

        foreach (var item in itensAsArray.Select((asset, i) => new { asset, i }))
        {
            if (item.i == 0) continue;

            if (orderByCrescent) {
                Assert.True(itensAsArray[item.i - 1].CreatedAt <= item.asset.CreatedAt);
            }

            if (!orderByCrescent) {
                Assert.True(itensAsArray[item.i - 1].CreatedAt >= item.asset.CreatedAt);
            }
        }
    }

    private async Task DeleteAssets() {
        await Application.DbContextFixture.TruncateTable<AssetVersion>();
        await Application.DbContextFixture.TruncateTable<Asset>();
    }

    private async Task SetupData() {
        await DeleteAssets();

        Assets = AssetBuilder
            .Setup()
            .Materialize(Application.DbContextFixture)
            .BuildMany(AssetesCount);
    }

    #endregion
}
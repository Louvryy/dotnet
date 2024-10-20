using Louvryy.Core.Tests.Fixtures.DbContextFixture;
using Louvryy.Core.Tests.Fixtures.EntityBuilders;

namespace Louvryy.Core.Tests.MinimalApi.Seeders;

public static class AssetSeeder
{
    public static void Run(IServiceProvider services)
    {
        var db = services.GetRequiredService<TestDbContext>();
        var dbFixture = new DbContextFixture(db);

        if (db.Assets.Any()) return;

        AssetBuilder
            .Setup()
            .Materialize(dbFixture)
            .BuildMany(20);
    }
}
using Louvryy.Core.Tests.Fixtures.DbContextFixture;

namespace Louvryy.Core.Tests.MinimalApi.Seeders;

public static class DatabaseSeeder
{
    public static void Run(IServiceProvider services, string environmentName)
    {
        var db = services.GetRequiredService<TestDbContext>();

        AssetSeeder.Run(services);
        db.SaveChanges();
    }
}

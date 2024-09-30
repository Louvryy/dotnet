using Louvryy.Core.Tests.Fixtures.DbContextFixture;

namespace Louvryy.Core.Tests.Data.Fixtures;

public class ApplicationFixture : IDisposable, IAsyncLifetime {
    public readonly DbContextFixture DbContextFixture;

    public ApplicationFixture(TestDbContext dbContext) {
        DbContextFixture = new(dbContext);
    }

    public void Dispose()
    {
        //
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public virtual async Task InitializeAsync()
    {
        await InitializeDatabase();
    }

    public async Task InitializeDatabase()
    {
        await DbContextFixture.RefreshDatabaseAsync();
    }
}
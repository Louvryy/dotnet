using Moq;
using Louvryy.Core.Tests.Helpers;
using Louvryy.Core.Tests.Fixtures.DbContextFixture;
using Microsoft.Extensions.DependencyInjection;

namespace Louvryy.Core.Tests.Domain.Fixtures;

public class ApplicationFixture : IDisposable, IAsyncLifetime
{
    public readonly IServiceProvider ServiceProvider;
    public readonly DbContextFixture DbContextFixture;

    public ApplicationFixture(
        IServiceProvider serviceProvider,
        TestDbContext dbContext
    ) {
        ServiceProvider = serviceProvider;

        DbContextFixture = new(dbContext);
    }

    public void Dispose()
    {
        //
    }

    public TService GetService<TService>() where TService : class
    {
        return ServiceProvider.GetRequiredService<TService>();
    }

    public (IServiceProvider, Mock<TService>) MockService<TService>(Action<Mock<TService>> action) where TService : class
    {
        return ServiceMocker.Mock(
            (sc) => new Startup().ConfigureServices(sc),
            action
        );
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
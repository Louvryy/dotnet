using Moq;
using Louvryy.Core.Tests.Helpers;

namespace Louvryy.Core.Tests.Domain.Fixtures;

public class ApplicationFixture : IDisposable, IAsyncLifetime
{

    public ApplicationFixture() {
    }

    public void Dispose()
    {
        //
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
        await Task.CompletedTask;
    }
}
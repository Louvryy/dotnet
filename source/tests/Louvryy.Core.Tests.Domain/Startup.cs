using Louvryy.Core.Api;
using Louvryy.Core.Tests.Configurations;
using Louvryy.Core.Tests.Fixtures.DbContextFixture;
using Microsoft.Extensions.DependencyInjection;

namespace Louvryy.Core.Tests.Domain;

public class Startup {
    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureConfigurationFixture();
        services.ConfigureDbContext();
        services.ConfigureLouvryy(cfg => {
            cfg.ConfigureData<TestDbContext>();
        });
    }
}
using Louvryy.Core.Api;
using Louvryy.Core.Tests.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Louvryy.Core.Tests.Data;

public class Startup {
    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureConfigurationFixture();
        services.ConfigureDbContext();
        services.ConfigureLouvryy();
    }
}
using Louvryy.Core.Tests.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Louvryy.Core.Tests.Domain;

public class Startup {
    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureConfigurationFixture();
    }
}
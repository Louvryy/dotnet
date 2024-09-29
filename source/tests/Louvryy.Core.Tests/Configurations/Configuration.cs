using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Louvryy.Core.Tests.Configurations;

public static class Configurations
{
    public static void ConfigureConfigurationFixture(this IServiceCollection services) {
        var Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Tests.json")
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton<IConfiguration>(Configuration);
    }
}

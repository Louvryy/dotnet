using Louvryy.Core.Tests.Fixtures.DbContextFixture;
using Microsoft.Extensions.DependencyInjection;

namespace Louvryy.Core.Tests.Configurations;

public static class DbContextConfiguration {
    public static void ConfigureDbContext(this IServiceCollection services) {
        services.AddDbContext<TestDbContext>();
    }
}
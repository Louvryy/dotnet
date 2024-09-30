using Louvryy.Core.Data.Repositories;
using Louvryy.Core.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Louvryy.Core.Api;

public static class LouvryyBuilder
{
    public static IServiceCollection ConfigureLouvryy(this IServiceCollection services, Action<ILouvryyBuilderOptions> setupAction = null)
    {
        LouvryyBuilderOptions options = new();

        setupAction?.Invoke(options);

        Console.WriteLine("Hello Louvryy!");

        if (options.DbContextType is not null)
            ConfigureData(services, options.DbContextType);

        return services;
    }

    private static void ConfigureData(IServiceCollection services, Type dbContextType)
    {
        RegisterRepository(services, typeof(IAssetRepository), typeof(AssetRepository<>), dbContextType);
    }

    private static void RegisterRepository(IServiceCollection services, Type repositoryInterface, Type repositoryImplementation, Type dbContextType)
    {
        var closedImplementationType = repositoryImplementation.MakeGenericType(dbContextType);
        services.AddTransient(repositoryInterface, closedImplementationType);
    }
}

public class LouvryyBuilderOptions : ILouvryyBuilderOptions
{
    public Type? DbContextType;

    public ILouvryyBuilderOptions ConfigureData<TDbContext>() where TDbContext : DbContext
    {
        DbContextType = typeof(TDbContext);
        return this;
    }
}

public interface ILouvryyBuilderOptions {
    ILouvryyBuilderOptions ConfigureData<TDbContext>() where TDbContext : DbContext;
}
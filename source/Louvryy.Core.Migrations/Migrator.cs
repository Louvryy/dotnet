using FluentMigrator.Runner;
using FluentMigrator.Runner.Logging;
using Louvryy.Core.Migrations.Scripts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Louvryy.Core.Migrations;

public class Migrator
{

    public static void RunUpdate(string connection, bool showLogs = false)
    {
        var serviceProvider = CreateServices(connection, showLogs);

        // Put the database update into a scope to ensure
        // that all resources will be disposed.
        using (var scope = serviceProvider.CreateScope())
        {
            UpdateDatabase(scope.ServiceProvider);
        }
    }

    /// <summary>
    /// Configure the dependency injection services
    /// </summary>
    private static ServiceProvider CreateServices(string connection, bool showLogs)
    {
        var serviceCollection = new ServiceCollection()
            // Add common FluentMigrator services
            .AddFluentMigratorCore()
            .ConfigureRunner(ctx =>
            {
                ctx.AddSqlServer();
                ctx.WithGlobalConnectionString(connection);
                ctx.ScanIn(typeof(AddAssetTable).Assembly).For.Migrations();
            });

        if (showLogs)
        {

            serviceCollection.AddSingleton<ILoggerProvider, FluentMigratorConsoleLoggerProvider>()
                .Configure<FluentMigratorLoggerOptions>(opt =>
                {
                    opt.ShowSql = true;
                    opt.ShowElapsedTime = true;
                });
        }


        return serviceCollection.BuildServiceProvider(false);
    }

    /// <summary>
    /// Update the database
    /// </summary>
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        runner.MigrateUp();
    }

}
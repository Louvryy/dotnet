using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Louvryy.Core.Migrations;
using Louvryy.Core.Api;
using Louvryy.Core.Tests.MinimalApi.Configurations;
using Louvryy.Core.Tests.Fixtures.DbContextFixture;
using AutoMapper;
using Louvryy.Core.Tests.MinimalApi.Seeders;

namespace Louvryy.Core.Tests.MinimalApi;

/// <summary>
/// Program
/// </summary>
public class Program
{
    /// <summary>
    /// Main
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.UseUrls("http://localhost:5000");

        // Add services to the container.

        builder.Services
            .AddControllers();

        builder.Services.AddLogging(builder => builder.AddConsole());
        builder.Services.ConfigureSwagger(Assembly.GetExecutingAssembly().GetName().Name);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<TestDbContext>();

        builder.Services.ConfigureLouvryy(cfg => {
            cfg.ConfigureData<TestDbContext>();
        });

        ConfigureMapper(builder.Services, "Louvryy.Core.Tests.MinimalApi");

        builder.Services.ConfigureAuthentication();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            Migrator.RunUpdate(app.Configuration.GetConnectionString("TestConnection"));

            using IServiceScope scope = app.Services.CreateScope();

            IServiceProvider services = scope.ServiceProvider;

            DatabaseSeeder.Run(services, app.Environment.EnvironmentName);
        }


        app.MapControllers();
        app.Run();
    }

    private static void ConfigureMapper(IServiceCollection services, params string[] assemblyNames)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(assemblyNames);
        });

        configuration.AssertConfigurationIsValid();

        services.AddSingleton(configuration.CreateMapper());
    }
}

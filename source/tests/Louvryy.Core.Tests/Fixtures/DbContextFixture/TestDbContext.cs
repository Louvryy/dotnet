using Louvryy.Core.Data.DbContext;
using Louvryy.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Louvryy.Core.Tests.Fixtures.DbContextFixture;

public class TestDbContext : DbContext {
    private IConfiguration _config { get; }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetVersion> AssetVersions { get; set; }

    public TestDbContext(IConfiguration config) {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("TestConnection"));
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        LouvryyDbContext.ConfigureStandardModels(builder);
    }
}
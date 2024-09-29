using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Louvryy.Core.Data.Models;

namespace Louvryy.Core.Data.DbContext;

public class LouvryyDbContext
{
    public static void ConfigureStandardModels(ModelBuilder builder)
    {
        builder.Entity<Asset>(ConfigureAsset);
        builder.Entity<AssetVersion>(ConfigureAssetVersion);
    }

    public static void ConfigureAsset(EntityTypeBuilder<Asset> builder)
    {
        builder.Ignore(a => a.Versions);
    }

    public static void ConfigureAssetVersion(EntityTypeBuilder<AssetVersion> builder) {
        builder.HasKey(av => new { av.OriginalAssetId, av.VersionAssetId });

        builder
            .HasOne(a => a.Version)
            .WithMany("Versions")
            .HasForeignKey(av => av.VersionAssetId)
            .OnDelete(DeleteBehavior.NoAction) // Solve this
            .HasPrincipalKey(a => a.Id);

        builder
            .HasOne(av => av.Original)
            .WithOne()
            .HasForeignKey<AssetVersion>(av => av.OriginalAssetId)
            .OnDelete(DeleteBehavior.NoAction) // Solve this
            .HasPrincipalKey<Asset>(a => a.Id);
    }
}
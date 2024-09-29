namespace Louvryy.Core.Data.Models;

public class AssetVersion {
    public Guid OriginalAssetId { get; set; }
    public Guid VersionAssetId { get; set; }
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; }

    // Relations
    public virtual Asset Original { get; set; }
    public virtual Asset Version { get; set; }
}
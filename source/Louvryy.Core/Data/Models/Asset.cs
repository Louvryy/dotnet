namespace Louvryy.Core.Data.Models;

public class Asset {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string? Caption { get; set; }
    public string? Alt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public virtual IEnumerable<AssetVersion> Versions { get; set; }
}
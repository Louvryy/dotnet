namespace Louvryy.Core.Domain.DTOs;

public record AssetDTO {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string? Caption { get; set; }
    public string? Alt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
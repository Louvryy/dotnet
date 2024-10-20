namespace Louvryy.Core.DTOs;

public record PaginationDTO<TObject> {
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public int LastPage { get; set; }
    public IEnumerable<TObject> Items { get; set; }
}
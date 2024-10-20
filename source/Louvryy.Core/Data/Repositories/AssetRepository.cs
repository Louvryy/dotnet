using Microsoft.EntityFrameworkCore;
using Louvryy.Core.Domain.Interfaces.Repositories;
using Louvryy.Core.Data.Models;
using Louvryy.Core.Data.Utils;
using Louvryy.Core.DTOs;

using EFDbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Louvryy.Core.Data.Repositories;

public class AssetRepository<TDbContext>(
    TDbContext dbContext
    ) : IAssetRepository where TDbContext : EFDbContext
{
    private readonly TDbContext _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly DbSet<Asset> _model = dbContext.Set<Asset>();

    /// <summary>
    /// Paginate original assets
    /// </summary>
    /// <param name="perPage"></param>
    /// <param name="page"></param>
    /// <param name="search"></param>
    /// <param name="orderByCrescent"></param>
    /// <returns></returns>
    public Pagination<Asset> PaginateOriginals(
        int perPage,
        int page,
        string? search = null,
        bool? orderByCrescent = true
    )
    {
        var query = _model
            .Where(
                a => !_db.Set<AssetVersion>()
                    .Select(av => av.VersionAssetId)
                    .Contains(a.Id)
            );

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(
                a => EF.Functions.Like(a.Name, $"%{search}%")
                  || EF.Functions.Like(a.Title, $"%{search}%")
            );
        }

        if (orderByCrescent is not null && (bool)orderByCrescent)
        {
            query = query.OrderBy(a => a.CreatedAt);
        }

        if (orderByCrescent is not null && !(bool)orderByCrescent)
        {
            query = query.OrderByDescending(a => a.CreatedAt);
        }

        return new Pagination<Asset>(query, page, perPage);
    }
}
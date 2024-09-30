using Louvryy.Core.DTOs;
using Louvryy.Core.Data.Models;

namespace Louvryy.Core.Domain.Interfaces.Repositories;

public interface IAssetRepository
{
    /// <summary>
    /// Paginate original assets
    /// </summary>
    /// <param name="perPage"></param>
    /// <param name="page"></param>
    /// <param name="search"></param>
    /// <param name="orderByCrescent"></param>
    /// <returns></returns>
    PaginationDTO<Asset> PaginateOriginals(
        int perPage,
        int page,
        string? search = null,
        bool? orderByCrescent = true
    );
}
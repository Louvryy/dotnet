using AutoMapper;
using Louvryy.Core.DTOs;
using Louvryy.Core.Domain.DTOs;
using Louvryy.Core.Data.Models;
using Louvryy.Core.Data.Utils;

namespace Louvryy.Core.Mappers;

public class DomainProfile : Profile {
    public DomainProfile() {

        CreateMap(typeof(Pagination<>), typeof(PaginationDTO<>));

        CreateMap<Asset, AssetDTO>(MemberList.Destination);
    }
}
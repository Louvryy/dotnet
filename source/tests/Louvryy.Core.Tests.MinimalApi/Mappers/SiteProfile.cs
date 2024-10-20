using AutoMapper;
using Louvryy.Core.DTOs;
using Louvryy.Core.Domain.DTOs;
using Louvryy.Core.Tests.MinimalApi.Responses;

namespace Louvryy.Core.Tests.MinimalApi.Mappers;

public class SiteProfile : Profile {
    public SiteProfile() {

        CreateMap(typeof(PaginationDTO<>), typeof(PaginationResponse<>));

        CreateMap<AssetDTO, AssetResponse>(MemberList.Destination)
            .ForMember(
                dest => dest.Url,
                cfg => cfg.Ignore()
            );
    }
}
using AutoMapper;
using UserService.Application.DTOs;

namespace UserService.Application.Mappings
{
    public class GetUsersQueryMappingProfile : Profile
    {
        public GetUsersQueryMappingProfile()
        {
            CreateMap<GetUsersQueryDto, Dictionary<string, string>>()
                .ForMember(dest => dest["search"], opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Search)))
                .ForMember(dest => dest["skip"], opt => opt.MapFrom(src => src.Skip.ToString()))
                .ForMember(dest => dest["take"], opt => opt.MapFrom(src => src.Take.ToString()));
        }
    }
}

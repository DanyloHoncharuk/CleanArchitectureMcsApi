using AutoMapper;
using UserService.Application.DTOs;
using UserService.Common;

namespace UserService.Application.Mappings
{
    public class GetUsersDtoMappingProfile : Profile
    {
        public GetUsersDtoMappingProfile()
        {
            CreateMap<GetUsersDto, Dictionary<string, string>>()
                .AfterMap((src, dest) =>
                {
                    dest[GetUsersQueryParameters.Search] = !string.IsNullOrEmpty(src.Search) ? src.Search : "";
                    dest[GetUsersQueryParameters.Skip] = src.Skip.ToString();
                    dest[GetUsersQueryParameters.Take] = src.Take.ToString();
                });
        }
    }
}

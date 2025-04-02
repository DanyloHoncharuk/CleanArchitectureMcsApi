using AutoMapper;
using UserService.Application.DTOs;

namespace UserService.Application.Mappings
{
    public class GetUsersDtoMappingProfile : Profile
    {
        public GetUsersDtoMappingProfile()
        {
            CreateMap<GetUsersDto, Dictionary<string, string>>()
                .AfterMap((src, dest) =>
                {
                    dest["search"] = !string.IsNullOrEmpty(src.Search) ? src.Search : "";
                    dest["skip"] = src.Skip.ToString();
                    dest["take"] = src.Take.ToString();
                });
        }
    }
}

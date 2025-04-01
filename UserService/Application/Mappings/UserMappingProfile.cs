using AutoMapper;
using UserService.Common;
using UserService.Application.DTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString(DateFormats.DateOnly)))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString(DateFormats.DateTimeWithMilliseconds)))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => 
                    src.UpdateDate.HasValue ? src.UpdateDate.Value.ToString(DateFormats.DateTimeWithMilliseconds) : null));

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.Parse(src.DateOfBirth)))
                .AfterMap((src, dest) => dest.SetPassword(src.Password));
        }

    }
}

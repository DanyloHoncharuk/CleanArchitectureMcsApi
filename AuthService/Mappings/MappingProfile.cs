using AuthService.DTO;
using AuthService.Models;
using AuthService.Services;
using AutoMapper;

namespace AuthService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => UserPasswordService.HashPassword(src.Password)));
        }
    }
}

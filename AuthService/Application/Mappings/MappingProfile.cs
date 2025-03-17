using AuthService.Application.DTOs;
using AuthService.Application.Services;
using AuthService.Domain.Models;
using AutoMapper;

namespace AuthService.Application.Mappings
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

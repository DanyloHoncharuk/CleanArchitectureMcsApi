using AutoMapper;
using UserService.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationResult> GetUsers(GetUsersQueryDto getUsersQueryDto)
        {
            var parameters = _mapper.Map<Dictionary<string, string>>(getUsersQueryDto);
            var users = await _userRepository.GetUsersAsync(parameters);

            return new OperationResult { Success = true, Message = null, Data = _mapper.Map<List<UserDto>>(users) };
        }
    }
}

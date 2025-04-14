using AuthService.Application.DTOs;
using AuthService.Application.Wrappers;
using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult<object?>> CreateUserAsync(CreateUserDto createUserDto);
        Task<OperationResult<object>> AuthenticateUserAsync(AuthenticateUserDto authenticateUserDto);
    }
}

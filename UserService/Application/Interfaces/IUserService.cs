using UserService.Application.Common;
using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult> GetUsersAsync(GetUsersDto getUsersQueryDto);
        Task<OperationResult> GetUserByIdAsync(string id);
        Task<OperationResult> CreateUserAsync(CreateUserDto createUserDto);
        Task<OperationResult> UpdateUserAsync(string id, UpdateUserDto updateUserDto);
        Task<OperationResult> DeleteUserAsync(string id);
    }
}

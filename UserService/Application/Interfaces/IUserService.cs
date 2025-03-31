using UserService.Application.Common;
using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult> GetUsers(GetUsersQueryDto getUsersQueryDto);
    }
}

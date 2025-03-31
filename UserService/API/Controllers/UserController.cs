using Microsoft.AspNetCore.Mvc;
using UserService.Application.Common;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.API.Controllers
{
    public class UserController(IUserService userService) : BaseApiController
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersQueryDto getUsersQueryDto)
        {
            var result = await _userService.GetUsers(getUsersQueryDto);

            return result.Success ? Ok(result) : BadRequest();
        }
    }
}

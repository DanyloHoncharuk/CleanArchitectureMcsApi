using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.API.Controllers
{
    public class UsersController(IUserService userService) : BaseApiController
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] GetUsersDto getUsersDto)
        {
            var result = await _userService.GetUsersAsync(getUsersDto);

            return result.Success ? Ok(result) : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            return result.Success ? Ok(result) : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            var result = await _userService.CreateUserAsync(createUserDto);

            return result.Success ? Ok(result) : BadRequest();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] UpdateUserDto updateUserDto)
        {
            var result = await _userService.UpdateUserAsync(id, updateUserDto);

            return result.Success ? Ok(result) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            return result.Success ? Ok(result) : BadRequest();
        }
    }
}

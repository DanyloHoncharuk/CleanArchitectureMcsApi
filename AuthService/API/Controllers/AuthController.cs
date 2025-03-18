using Microsoft.AspNetCore.Mvc;
using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;

namespace AuthService.API.Controllers
{
    public class AuthController(IUserService userService) : BaseApiController
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            var result = await _userService.CreateUserAsync(createUserDto);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        //[HttpPost("authenticate")]
        //public async Task<IActionResult> AuthenticateUserAsync([FromBody] AuthenticateUserDto authenticateUserDto)
        //{
        //    var user = await _userService.GetUserByUsernameAsync(authenticateUserDto.Username);
        //    if (user != null && UserPasswordService.VerifyPassword(authenticateUserDto.Password, user.Password))
        //    {
        //        var token = _jwtService.GenerateToken(authenticateUserDto.Username);
        //        return Ok(new { jwt = token });
        //    }

        //    return Unauthorized();
        //}
    }
}

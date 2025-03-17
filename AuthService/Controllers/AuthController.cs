using Microsoft.AspNetCore.Mvc;
using AuthService.Data;
using AuthService.DTO;
using AuthService.Services;
using AutoMapper;
using AuthService.Models;
using Azure.Core;


namespace AuthService.Controllers
{
    public class AuthController(AuthServiceDbContext context,
                          UserService userService,
                          JwtService jwtService,
                          IMapper mapper) : BaseApiController(context)
    {
        private readonly UserService _userService = userService;
        private readonly JwtService _jwtService = jwtService;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            if (await _userService.IsUsernameTakenAsync(createUserDto.Username)) 
                return BadRequest(new {message = $"User with username {createUserDto.Username} is already exist." });

            if(createUserDto.Email != null && await _userService.IsEmailTakenAsync(createUserDto.Email))
                return BadRequest(new { message = $"User with email {createUserDto.Email} is already exist." });

            var user = _mapper.Map<User>(createUserDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new {message = $"User {user.Username} was added successfully." });
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUserAsync([FromBody] AuthenticateUserDto authenticateUserDto)
        {
            var user = await _userService.GetUserByUsernameAsync(authenticateUserDto.Username);
            if(user != null && UserPasswordService.VerifyPassword(authenticateUserDto.Password, user.Password))
            {
                var token = _jwtService.GenerateToken(authenticateUserDto.Username);
                return Ok(new { jwt = token });
            }

            return Unauthorized();
        }
    }
}

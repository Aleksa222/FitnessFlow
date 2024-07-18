using Microsoft.AspNetCore.Mvc;
using FitnessFlowBackend.DTOs;
using FitnessFlowBackend.Services.Interfaces;
using FitnessFlowBackend.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace FitnessFlowBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var user = await _userService.Register(userDTO.Email, userDTO.Password);

            if (user == null) return BadRequest("That email is already registered!");

            var token = GenerateJwtToken(user);

            return Ok(new { token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            var user = await _userService.Login(userDTO.Email, userDTO.Password);

            if (user == null) return BadRequest("Invalid email or password");

            var token = GenerateJwtToken(user);
            return Ok(new { token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(25),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}

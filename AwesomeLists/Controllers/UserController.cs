using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AwesomeLists.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AwesomeLists.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new { test = "test"});
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel userLoginModel)
        {
            var user = Authentificate(userLoginModel);

            if (user != null)
            {
                string token = GenerateJwt(userLoginModel);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GenerateJwt(UserLoginModel userLoginModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, userLoginModel.Login),
                 //new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
                 //new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                 
                 // ROLE claim ???
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                 _configuration["Jwt:Issuer"],
                 claims,
                 expires: DateTime.Now.AddMinutes(120),
                 signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserLoginModel Authentificate(UserLoginModel userLoginModel)
        {
            if(userLoginModel.Login == "user1" && userLoginModel.Password == "qwerty")
            {
                return userLoginModel;
            }
            return null;
        }
    }
}
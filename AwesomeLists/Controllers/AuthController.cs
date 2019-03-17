using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AwesomeLists.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
// using AuthUser = Microsoft.AspNetCore.Identity.IdentityUser; //<System.Guid>;

namespace AwesomeLists.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly UserManager<AuthUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(SignInManager<AuthUser> signInManager,
            UserManager<AuthUser> userManager,
            IConfiguration configuration)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Microsoft.AspNetCore.Identity.SignInResult result =
                await _signInManager.PasswordSignInAsync(loginModel.Login, loginModel.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            AuthUser authUser = _userManager.Users.FirstOrDefault(user => user.UserName == loginModel.Login);
            string token = GenerateJwtTokenString(authUser);

            return Ok(new { user = "domain user should be here", token });
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authUser = new AuthUser
            {
                UserName = loginModel.SignInModel.Login
            };

            var result = await _userManager.CreateAsync(authUser);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "user with such login already exists" });
            }

            await _signInManager.SignInAsync(authUser, false);
            string token = GenerateJwtTokenString(authUser);

            return Ok(new { user = "domain user should be here", token });
        }

        private string GenerateJwtTokenString(AuthUser user)
        {
            long unixNowSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
            long expirationTime = unixNowSeconds + 10 * 3600;

            var claims = new List<Claim>
            {
                // https://openid.net/specs/openid-connect-core-1_0.html#IDToken
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                //new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"]),
                //new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, unixNowSeconds.ToString()),
                //new Claim(JwtRegisteredClaimNames.Exp, expirationTime.ToString()),
                // new Claim(ClaimTypes.Role, user.)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
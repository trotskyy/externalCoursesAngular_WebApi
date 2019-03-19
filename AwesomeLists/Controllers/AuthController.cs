using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AwesomeLists.Data.Entities;
using AwesomeLists.Services.Auth;
using AwesomeLists.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AwesomeLists.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly UserManager<AuthUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _appUserService;

        public AuthController(SignInManager<AuthUser> signInManager,
            UserManager<AuthUser> userManager,
            IUserService appUserService,
            IConfiguration configuration)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _appUserService = appUserService ?? throw new ArgumentNullException(nameof(appUserService));
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync([FromBody] SignInModel signInModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Microsoft.AspNetCore.Identity.SignInResult result =
                await _signInManager.PasswordSignInAsync(signInModel.Login, signInModel.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            AuthUser authUser = _userManager.Users.FirstOrDefault(user => user.UserName == signInModel.Login);
            User appUser = await _appUserService.GetByIdAsync(authUser.Id);
            string token = GenerateJwtTokenString(authUser);

            return Ok(new { user = appUser, token });
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authUser = new AuthUser
            {
                UserName = signUpModel.SignInModel.Login
            };

            var result = await _userManager.CreateAsync(authUser, signUpModel.SignInModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(authUser, false);
            string authUserId = _userManager.Users.FirstOrDefault(user => user.UserName == authUser.UserName).Id;

            var appUser = new User
            {
                Id = authUserId,
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName
            };

            await _appUserService.AddAsync(appUser);

            string token = GenerateJwtTokenString(authUser);

            return Ok(new { user = appUser, token });
        }

        private string GenerateJwtTokenString(AuthUser user)
        {
            long unixNowSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
            long expirationTime = unixNowSeconds + 10 * 3600;

            var claims = new List<Claim>
            {
                // https://openid.net/specs/openid-connect-core-1_0.html#IDToken
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                //new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"]),
                //new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, unixNowSeconds.ToString()),
                //new Claim(JwtRegisteredClaimNames.Exp, expirationTime.ToString()),
                // new Claim(ClaimTypes.Role, user.)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AwesomeLists.Data.Entities;
using AwesomeLists.Services.Auth;
using AwesomeLists.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace AwesomeLists.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _appUserService;
        private readonly UserManager<AuthUser> _userManager;

        public UserController(IUserService appUserService, UserManager<AuthUser> userManager)
        {
            _appUserService = appUserService ?? throw new ArgumentNullException(nameof(appUserService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUserAsync(CancellationToken token)
        {
            string currentUserId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (currentUserId == null)
            {
                return NotFound();
            }

            token.ThrowIfCancellationRequested();

            User currentAppUser = await _appUserService.GetByIdAsync(currentUserId, token);

            return Ok(currentAppUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            IReadOnlyCollection<User> allUsers = await _appUserService.GetAllAsync(token);

            return Ok(allUsers);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteUserAsync([Required]string userId, CancellationToken token)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            token.ThrowIfCancellationRequested();

            Task<User> userTask = _appUserService.GetByIdAsync(userId, token);
            Task<AuthUser> authUserTask = _userManager.FindByIdAsync(userId);

            await Task.WhenAll(userTask, authUserTask);

            User user = userTask.Result;
            AuthUser authUser = authUserTask.Result;

            if (user == null || authUser == null)
            {
                return NotFound();
            }

            if (user.Id != authUser.Id)
            {
                throw new Exception("Domain constraint violation. Auth and domain users have different IDs");
            }

            token.ThrowIfCancellationRequested();

            var deleteDomainUserTask = _appUserService.DeleteAsync(user, token);
            var deleteAuthUserTask = _userManager.DeleteAsync(authUser);

            await Task.WhenAll(deleteDomainUserTask, deleteAuthUserTask);

            return NoContent();
        }
    }
}
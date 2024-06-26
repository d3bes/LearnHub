using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LearnHub.Api.Extensions;
using System.Security.Claims;
using LearnHub.Core.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LearnHub.Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly  ILogger<UsersController> _logger;
        //         {
        //   "email": "Admin.test@sec.com",
        //   "password": "Admin_pwd1"}


        public UsersController(UserManager<IdentityUser> userManager , ILogger<UsersController> logger, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {

            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
                    _logger = logger;

        }
        [Authorize(Roles = Role.admin)]

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<UserDto>> GetAllUsers()
        {
            try
            {
                  var roles = await _roleManager.Roles.ToListAsync();
                  var UsersGroups = new List<GroupUsersByRoleDto>(); 
                    foreach (var role in roles)
                    {
                       var users= await _userManager.GetUsersInRoleAsync(role.Name);
                        var groupUsersByRole = new GroupUsersByRoleDto(){
                            roleName = role.Name,
                            Users = users.ToUserDtoList()
                        };
                        UsersGroups.Add(groupUsersByRole);
                    }

                if(!UsersGroups.IsNullOrEmpty())
                    {
                 _logger.LogInformation("Load Users Successfully.");
                    }
                return Ok(UsersGroups);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching users: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            UserDto userDto = user.ToUserDto();
            return Ok(userDto);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserDto updateUserDto)
        {
            ClaimsPrincipal currentUserClaims = this.User;
            var currentUserID = currentUserClaims.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUserID != updateUserDto.Id)
            {
                return BadRequest("User ID mismatch");
            }

            var user = await _userManager.FindByIdAsync(currentUserID);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.UserName = updateUserDto.UserName;
            user.Email = updateUserDto.Email;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(true);
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("Update/{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto updateUserDto)
        {
            if (userId != updateUserDto.Id)
            {
                return BadRequest("User ID mismatch");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.UserName = updateUserDto.UserName;
            user.Email = updateUserDto.Email;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(true);
            }

            return BadRequest(result.Errors);
        }



        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (result.Succeeded)
            {
                return Ok("Password changed successfully");
            }

            return BadRequest(result.Errors);
        }


        [HttpPut("{id}/change-email")]
        public async Task<IActionResult> ChangeEmail(string id, [FromBody] ChangeEmailDto changeEmailDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, changeEmailDto.NewEmail);
            var result = await _userManager.ChangeEmailAsync(user, changeEmailDto.NewEmail, token);

            if (result.Succeeded)
            {
                user.UserName = changeEmailDto.NewEmail; // Optional: Update username if needed
                await _userManager.UpdateAsync(user);
                return Ok("Email changed successfully");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("{id}/confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string id, [FromQuery] string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully");
            }

            return BadRequest(result.Errors);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
         private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

          [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
         [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id)
            {
                return BadRequest("User ID mismatch");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.UserName = updateUserDto.UserName;
            user.Email = updateUserDto.Email;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("User updated successfully");
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

    }
}
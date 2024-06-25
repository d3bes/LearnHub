using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LearnHub.EF.Migrations;
using LearnHub.Core.Models;
using LearnHub.Core.Dto;
using Microsoft.Identity.Client;
using LearnHub.Core.Interfaces;

namespace LearnHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // private readonly RoleManager<IdentityUser> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<IdentityUser> userManager, ITokenService tokenService)

        {

            // _roleManager = roleManager;

            _userManager = userManager;
            _tokenService = tokenService;

        }
        [HttpPost("addAdminPassword")]
        public async Task<IActionResult> AddAdminPasswordForFirstTimeAsync(string password) //Admin_pwd1
        {
            string adminId = "07a700e7-6f45-44bf-a4f1-76f5f754fe3f";
            IdentityUser user = await _userManager.FindByIdAsync(adminId);

            if (user == null)
            {
                return BadRequest("Admin user not found.");
            }

            user = await _userManager.FindByIdAsync(adminId);

            if (user.PasswordHash == null)
            {
                var result = await _userManager.AddPasswordAsync(user, password);

                if (result.Succeeded)
                {
                    user = await _userManager.FindByIdAsync(adminId); // Refresh user data
                    var token = _tokenService.CreateAdminToken(user, _userManager);

                    return Ok(token); // Return the updated password hash

                }
                else
                {
                    return BadRequest($"Failed to set password for admin user: {result.Errors}");
                }
            }
            else
            {
                return BadRequest("Admin user already has a password set.");
            }
        }







    }


}
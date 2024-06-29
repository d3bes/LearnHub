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
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using LearnHub.Core.Consts;
using LearnHub.Api.Extensions;


namespace LearnHub.Api.Controllers
{
    // [Authorize(Roles = "Administrator")]

    [Authorize(Roles = Role.admin)]
    [ApiController]
    [Route("api/")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        //         {
        //   "email": "Admin.test@sec.com",
        //   "password": "Admin_pwd1"}

        public RoleController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {

            _roleManager = roleManager;

            _userManager = userManager;
        }


        [HttpGet("Roles")]
        public async Task<IActionResult> getRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }


        [HttpGet("UserRole")]
        public async Task<IActionResult> GetUserRole()
        {
            // Get the current user
            ClaimsPrincipal currentUserClaims = this.User;
            var currentUserID = currentUserClaims.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUserID == null)
            {
                return NotFound("User not found");
            }
            var currentUser = await _userManager.FindByIdAsync(currentUserID);
            // Check if user is in a specific role
            bool isInRole = await _userManager.IsInRoleAsync(currentUser, "Administrator");

            // Optionally, you can also get all roles assigned to the user
            var roles = await _userManager.GetRolesAsync(currentUser);

            return Ok(new { Name = currentUser.UserName, Id = currentUser.Id, IsInRole = isInRole, Roles = roles });
        }

        [HttpGet("UsersInRole/{roleName}")]
        public async Task<IActionResult> GetUsersInRole(string roleName)
        {
            // Find the role by name
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound($"Role '{roleName}' not found");
            }

            // Get all users who belong to the role
            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

            var usersData = usersInRole.ToList();

            // var users = new List<UserDto>();
            // foreach (var user in usersData)
            // {
            //     var userDto = user.ToUserDto();
            //     users.Add(userDto);
            // }
            var users = usersData.ToUserDtoList();
            UsersDataAndCountDto usersData_count = new UsersDataAndCountDto()
            {
                usersData = users,
                usersCount = users.Count()
            };

            return Ok(usersData_count);
        }

        [HttpPost("Roles/AddRole")]
        public async Task<IActionResult> addRole(string roleName)
        {
            var RoleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!RoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
                var role = await _userManager.FindByNameAsync(roleName);
                return Ok(role);
            }
            else
                return BadRequest("Role already exists");

        }
        [HttpPost("Role/assignment/{userId}")]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {

            var role = await _roleManager.FindByNameAsync(roleName);

            var user = await _userManager.FindByIdAsync(userId);

            if (role != null)
            {
                bool isInRole = await _userManager.IsInRoleAsync(user, role.Name.ToString());
                if (!isInRole)
                {
                    if (role.Name.ToString() != Role.admin)
                    {

                        await _userManager.AddToRoleAsync(user, role.Name.ToString());
                        return Ok($"Role {role.Name}  Assigned to user {user.UserName}");
                    }
                    else
                        return Unauthorized();
                }
                else
                    return BadRequest($" !Duplicate: Role {role.Name} already Assigned to user {user.UserName}");

            }
            return BadRequest($"Not Found Role");


        }

        [HttpDelete("Roles/Delete/{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound("Role not found");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok($"Role {role.Name} deleted");
            }
            else
            {
                // Handle errors if deletion fails
                return BadRequest("Failed to delete role");
            }
        }

        [HttpDelete("RemoveUserRole")]
        public async Task<IActionResult> RemoveUserRole(string userId, string roleName)
        {
            // Find the user by Id
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"User with Id '{userId}' not found");
            }

            // Check if the user is currently in the role
            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                // Remove the user from the role
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    return Ok($"Removed role '{roleName}' from user '{user.UserName}'");
                }
                else
                {
                    return BadRequest($"Failed to remove role '{roleName}' from user '{user.UserName}': {string.Join(", ", result.Errors)}");
                }
            }
            else
            {
                return BadRequest($"User '{user.UserName}' is not in role '{roleName}'");
            }
        }

    }
}











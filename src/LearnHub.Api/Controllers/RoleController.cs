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

namespace LearnHub.Api.Controllers
{
    [ApiController]
    [Route("api/")]
    public class RoleController : ControllerBase
    {  
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public RoleController(UserManager<IdentityUser> userManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {

             _roleManager = roleManager;

            _userManager = userManager;
            _tokenService = tokenService;
        }

        // [Authorize(Roles ="Administrator")]
        [HttpGet("Roles")]
        public async Task<IActionResult> getRoles ()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }


        [HttpPost("Roles/AddRole")]
        public async Task<IActionResult> addRole(string roleName)
        {
            var RoleExists = await _roleManager.RoleExistsAsync(roleName);
            if(!RoleExists) 
            {
               await _roleManager.CreateAsync(new IdentityRole(roleName));
               return Ok(roleName);
            }
            else
            return  BadRequest("Role already exists");

        }
        // [Authorize(Roles = "Administrator")]
        [HttpPost("Role/assignment/{userId}")]
        public async Task<IActionResult> AssignRole(string userId, string roleId)
        {

           var role= await _roleManager.FindByNameAsync(roleId);
           
           var user = await _userManager.FindByIdAsync(userId);
           _userManager.AddToRoleAsync(user,role.Name.ToString());
           return Ok();

        }
     
    
      
        







    }


}
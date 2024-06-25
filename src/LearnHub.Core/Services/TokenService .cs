using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LearnHub.Core.Consts;
using LearnHub.Core.Interfaces;
using LearnHub.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace LearnHub.Core.Services
{
    public class TokenService : ITokenService
    {
    
         private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateAdminToken(IdentityUser admin, UserManager<IdentityUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim(ClaimTypes.GivenName, admin.UserName),
            }; // Private Claims (UserDefined)
           // await userManager.AddToRoleAsync(admin,Role.admin);

            var userRoles = await userManager.GetRolesAsync(admin);

            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));


            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

       var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, admin.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, admin.Email),
                new Claim("adminId", admin.Id)
            }
            .Union(authClaims);

        
            var token = new JwtSecurityToken(

                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:ExpirationInDays"])),
                claims: claims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );



            

            return new JwtSecurityTokenHandler().WriteToken(token);
       }

        

       
    
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace LearnHub.Core.Interfaces
{
    public interface ITokenService
    {
       Task<string> CreateAdminToken(IdentityUser admin, UserManager<IdentityUser> userManager);


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LearnHub.Core.Dto;
using LearnHub.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace LearnHub.Api.Extensions
{
    public static class UserExtension
    {
        public static UserDto ToUserDto(this IdentityUser user)
        {
            return new UserDto(){
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    emailConfirmed =  user.EmailConfirmed,
                    phoneNumber = user.PhoneNumber,
                    phoneNumberConfirmed = user.PhoneNumberConfirmed

            };
        }

        public static List<UserDto> ToUserDtoList(this IList<IdentityUser> users) 
        {
            List<UserDto> result = new List<UserDto>();
            foreach(var user in users)
            {
                result.Add(user.ToUserDto());
            }
            
            return result;
        }
    }
}
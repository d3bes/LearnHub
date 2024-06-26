using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Dto
{
    public class UsersDataAndCountDto
    {
        public List<UserDto> usersData { get; set; }
        public int usersCount { get; set; }

    }
}
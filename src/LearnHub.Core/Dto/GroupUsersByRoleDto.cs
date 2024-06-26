using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Dto
{
    public class GroupUsersByRoleDto
    {
        public List<UserDto> Users { get; set; }
        public string roleName { get; set; }
    }
}
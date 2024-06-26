using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Dto
{
    public class UpdateUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool emailConfirmed { get; set; }
        public string? phoneNumber { get; set; }
        public bool? phoneNumberConfirmed { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Dto
{
    public class ChangePasswordDto
    {
         public string CurrentPassword { get; set; } 
        public string NewPassword { get; set; }
    }
}
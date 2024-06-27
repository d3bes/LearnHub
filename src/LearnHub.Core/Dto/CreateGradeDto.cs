using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Dto
{
    public class CreateGradeDto
    {
        public float Score { get; set; }
        public int CourseId { get; set; }
        public string StudentId { get; set; }

    }
}
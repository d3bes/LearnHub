using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Dto
{
    public class GradeDto
    {
        public int GradeId { get; set; }
        public float Score { get; set; }
        public int CourseId { get; set; }
        public string GradeName { get; set; }
        public string? CourseName { get; set; }
        public string StudentId { get; set; }
        public string? StudentName { get; set; }
        public DateTime? dateTime { get; set; }



    }
}
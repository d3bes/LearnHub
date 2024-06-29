using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Dto
{
    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }
        public string studentId { get; set; }
        public int courseId { get; set; }

        public DateTime enrollmentDate { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }

    }
}
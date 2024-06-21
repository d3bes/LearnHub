using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public DateTime enrollmentDate { get; set; }
        public Guid studintId { get; set; }
        public User student { get; set; }
        public int courseId { get; set; }
        public Course course { get; set; }
    }
}
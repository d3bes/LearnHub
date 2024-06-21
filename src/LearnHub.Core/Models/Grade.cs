using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public float Score { get; set;} 
        public int CourseId { get; set; }
        public Course course { get; set; }
        public Guid UserId { get; set; }
        public User user { get; set; }
    }
}
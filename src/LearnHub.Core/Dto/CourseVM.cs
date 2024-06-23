using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Dto
{
    public class CourseVM
    {
     public int CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    // Foreign key to instructor (one-to-one with User)
    public string InstructorId { get; set; }
    }
}
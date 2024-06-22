using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnHub.Core.Models
{
    public class Grade
    {
       public int GradeId { get; set; }
    public float Score { get; set; } 

    // Foreign key to Course
    public int CourseId { get; set; }
    public Course Course { get; set; }

    public string StudentId { get; set; }
    public User Student { get; set; }   
    }
}
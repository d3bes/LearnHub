using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;


namespace LearnHub.Core.Models
{
    public class Module : IBaseEntity

    {
        public int ModuleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        public int CourseId { get; set; }
        public Course Course  { get; set; }

        public List<Lesson> Lessons { get; set; }

    [NotMapped]
    public string TableName { get; set; } 
    }
}
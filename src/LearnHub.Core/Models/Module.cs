using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Models
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        public int CourseId { get; set; }
        public Course Course  { get; set; }

        public List<Lesson> Lessons { get; set; }

    }
}
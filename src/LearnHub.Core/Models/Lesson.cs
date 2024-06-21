using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Models
{
    public class Lesson
    {
        public  int LessonId { get; set; }

        public string Title { get; set; }
        public string content { get; set; }


          public int ModuleId { get; set; }
          public Module module { get; set; }

          public List<Content> contents { get; set; }

    }
}
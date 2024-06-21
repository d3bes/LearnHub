using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHub.Core.Models
{
    public class Content
    {
        public int ContentId { get; set; }

        public string Title { get; set; }
        public string type { get; set; }
        public string filePath { get; set; }


          public  int LessonId { get; set; }
        public Lesson lesson { get; set; }
    }
}
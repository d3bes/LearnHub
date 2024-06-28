using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Dto;
using LearnHub.Core.Models;

namespace LearnHub.Api.Extensions
{
    public static class CourseExtensions
    {
        public static CourseDto ToCourseDto(this Course course)
        {
            var courseDto = new CourseDto()
            {
               CourseId = course.CourseId,
               Description = course.Description,
               InstructorId = course.InstructorId,
               Title = course.Title,


            };
            if (course.Instructor != null) { courseDto.InstructorName  = course.Instructor.Name; }

            return courseDto;

        }

         public static List<CourseDto> ToCourseListDto(this IEnumerable<Course> courses)
        {
            var courseDtoList = new List<CourseDto>();
            foreach (Course course in courses)
            {
                CourseDto courseDto = new CourseDto();
                courseDto = course.ToCourseDto();
                courseDtoList.Add(courseDto);
            }

            return courseDtoList;
        }

    }
}
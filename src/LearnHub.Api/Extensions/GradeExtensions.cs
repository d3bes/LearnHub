using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Dto;
using LearnHub.Core.Interfaces;
using LearnHub.Core.Models;

namespace LearnHub.Api.Extensions
{
    public static class GradeExtensions
    {

        public static GradeDto ToGradeDto(this Grade grade)
        {
            var gradeDto = new GradeDto()
            {
                CourseId = grade.CourseId,
                dateTime = grade.dateTime,
                GradeId = grade.GradeId,
                Score = grade.Score,
                StudentId = grade.StudentId,
                GradeName = grade.GradeName,

            };
             if (grade.Course != null) { gradeDto.CourseName = grade.Course.Title; }
            if (grade.Student != null) { gradeDto.StudentName = grade.Student.UserName; }

            return gradeDto;


        }
        public static List<GradeDto> ToGradsListDto(this IEnumerable<Grade> grades)
        {
            var gradesDtoList = new List<GradeDto>();
            foreach (Grade grade in grades)
            {
                GradeDto gradeDto = new GradeDto();
                gradeDto = grade.ToGradeDto();
                gradesDtoList.Add(gradeDto);
            }

            return gradesDtoList;
        }


    }
}
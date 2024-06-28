using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Dto;
using LearnHub.Core.Models;

namespace LearnHub.Api.Extensions
{
    public static class EnrollmentExtensions
    {
          public static EnrollmentDto ToEnrollmentDto(this Enrollment enrollment)
        {
            var enrollmentDto = new EnrollmentDto()
            {
                EnrollmentId = enrollment.EnrollmentId,
                courseId = enrollment.courseId,
                enrollmentDate = enrollment.enrollmentDate,
                studentId = enrollment.studentId,


            };
             if (enrollment.course != null) { enrollmentDto.CourseName = enrollment.course.Title; }
            if (enrollment.student != null) { enrollmentDto.StudentName =enrollment.student.Name; }

            return enrollmentDto;

        }
            public static List<EnrollmentDto> ToEnrollmentListDto(this IEnumerable<Enrollment> grades)
        {
            var enrollmentDtoList = new List<EnrollmentDto>();
            foreach (Enrollment enrollment in grades)
            {
                EnrollmentDto enrollmentDto = new EnrollmentDto();
                
                enrollmentDto = enrollment.ToEnrollmentDto();
                enrollmentDtoList.Add(enrollmentDto);
            }

            return enrollmentDtoList;
        }

    }
}
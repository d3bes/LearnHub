using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LearnHub.Core.Models;
using LearnHub.Core.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LearnHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IBaseRepository<Enrollment> _enrollmentRepository;
        private readonly IBaseRepository<Course> _courseRepository;
        private readonly IBaseRepository<User> _studentRepository;
        public EnrollmentController(IBaseRepository<Enrollment> enrollmentRepository
       , IBaseRepository<Course> courseRepository, IBaseRepository<User> studentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
        }

        [HttpPost("enrollments")]
        public async Task<IActionResult> CourseEnrollment(EnrollmentDto enrollmentDto)
        {
            if (enrollmentDto != null)
            {
                Enrollment enrollment = new Enrollment()
                {
                    courseId = enrollmentDto.courseId,
                    studentId = enrollmentDto.studentId,
                    enrollmentDate = DateTime.Now
                };
                var result = await _enrollmentRepository.addAsync(enrollment);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("enrollments/{student_id}")]
        public async Task<IActionResult> GetStudentCouresById(string student_id)
        {

            var enrollments = await _enrollmentRepository.findAllAsync(std => std.studentId == student_id, ["Course"]);
            List<Course> courses = new List<Course>();
            foreach (var item in enrollments)
            {
                if (item.course != null)
                {
                    courses.Add(item.course);
                }
                else
                {
                     
                var course =  await _courseRepository.findAsync( x => item.studentId == student_id );
                courses.Add(course);

                }
            }

            return Ok(courses);

        }

        [HttpDelete("Delete_enrollments/{student_id}")]
        public async Task<IActionResult> DeleteEnrollmentById(string student_id)
        {
            // var student =await  _studentRepository.findAsync(std => std.Id == student_id);
            var enrollment = await _enrollmentRepository.findAsync(std => std.studentId == student_id);
            _enrollmentRepository.Delete(enrollment);

            return Ok(enrollment);
        }


    }
}
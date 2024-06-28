using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LearnHub.Core.Models;
using LearnHub.Core.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using LearnHub.Core.Consts;
using LearnHub.Api.Extensions;

namespace LearnHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IBaseRepository<Enrollment> _enrollmentRepository;
        private readonly IBaseRepository<Course> _courseRepository;
        private readonly IBaseRepository<User> _studentRepository;
        private readonly UserManager<User> _userManager;

        public EnrollmentController(IBaseRepository<Enrollment> enrollmentRepository
       , IBaseRepository<Course> courseRepository, IBaseRepository<User> studentRepository, UserManager<User> userManager)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _userManager = userManager;

        }

        /// <summary>
        ///Retrieves all enrollments.
        /// </summary>
        
        [Authorize( Roles = Role.admin)] 
        [HttpGet("AllEnrollments")]
        public async Task<IActionResult> GetAllEnrollments()
        {

            var enrollments = await _enrollmentRepository.getAllAsync(["student", "course"]);
            return Ok(enrollments.ToEnrollmentListDto());
        }

        /// <summary>
        /// Retrieves enrollments for a specific student identified by student_id
        /// </summary>
        [HttpGet("enrollments/student/{student_id}")]
        public async Task<IActionResult> GetStudentEnrollments(string student_id)
        {

            var enrollments = await _enrollmentRepository.findAllAsync(x => x.studentId == student_id,["student", "course"]);
            return Ok(enrollments.ToEnrollmentListDto());

        }

        /// <summary>
        /// Handles enrollment of a student into a course.
        /// </summary>
        [HttpPost("CreateEnrollment")]

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

                // bool checkFound = await _courseRepository.foundAsync(enrollment.courseId);
                bool checkFound = await _enrollmentRepository.foundAsync(x => x.courseId == enrollmentDto.courseId && x.studentId == enrollmentDto.studentId);
                if (!checkFound)
                {
                    var result = await _enrollmentRepository.addAsync(enrollment);
                    return Ok(result);
                }
                return BadRequest("Student is already enrolled in this course.");
            }
            return BadRequest();
        }

        /// <summary>
        ///Retrieves courses enrolled by a specific student identified by student_id.
        /// </summary>

        [HttpGet("courses/student/{student_id}")]
        public async Task<IActionResult> GetStudentCourses(string student_id)
        {
            // var enrollments = await _enrollmentRepository.findAllAsync(std => std.studentId == student_id);

            var enrollments = await _enrollmentRepository.findAllAsync(std => std.studentId == student_id, ["student", "course"]);

            List<Course> courses = new List<Course>();
            foreach (var item in enrollments)
            {
                if (item.course != null)
                {
                    courses.Add(item.course);
                }
                else
                {

                    foreach (var x in enrollments)
                    {

                        var studentCourse = await _courseRepository.getByIdAsync(x.courseId);
                        courses.Add(studentCourse);


                    }



                }
            }

            return Ok(courses.ToCourseListDto());

        }


        /// <summary>
        ///Retrieves students enrolled in a specific course identified by courseId.
        /// </summary>
        [HttpGet("courses/{courseId}/students")]
        public async Task<IActionResult> GetCourseStudents(int courseId)
        {
            var enrollments = await _enrollmentRepository.findAllAsync(c => c.courseId == courseId);
            // var enrollments = await  _enrollmentRepository.findAllAsync( c => c.courseId == courseId, ["student"]);
            var students = new List<User>();
            foreach (var enroll in enrollments)
            {
                var student = await _userManager.FindByIdAsync(enroll.studentId);

                if (student != null)
                {
                    students.Add(student);
                }
            }

            if (students.Any())
            {
                return Ok(students.ToUserDtoList());
            }
            else
            {
                return NotFound(); // Return 404 if no students were found
            }
        }


        /// <summary>
        ///Deletes all enrollments for a specific student identified by student_id.
        /// </summary>
        [HttpDelete("enrollments/student/{student_id}")]
        public async Task<IActionResult> DeleteStudentEnrollmentsById(string student_id)
        {
            // var student =await  _studentRepository.findAsync(std => std.Id == student_id);
            var enrollment = await _enrollmentRepository.findAllAsync(std => std.studentId == student_id);
            _enrollmentRepository.DeleteRange(enrollment);

            return Ok(enrollment.ToEnrollmentListDto());
        }


        /// <summary>
        ///Deletes a specific enrollment identified by enrollment_id.
        /// </summary>

         [HttpDelete("enrollments/{enrollment_id}")]
        public async Task<IActionResult> DeleteEnrollment(int enrollment_id)
        {
            var enrollment = await _enrollmentRepository.getByIdAsync(enrollment_id);
            _enrollmentRepository.Delete(enrollment);
            return Ok("Successfully deleted element : " + " [ " + enrollment.EnrollmentId + " ]");
        }


    }
}
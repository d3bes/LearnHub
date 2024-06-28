using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Api.Extensions;
using LearnHub.Core.Dto;
using LearnHub.Core.Interfaces;
using LearnHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {

        private readonly IBaseRepository<Grade> _gradeRepository;
        private readonly IBaseRepository<Course> _courseRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger<GradeController> _logger;

        public GradeController(IBaseRepository<Grade> gradeRepository, IBaseRepository<Course> courseRepository
                            , IBaseRepository<User> userRepository, ILogger<GradeController> logger)
        {
            _logger = logger;
            _gradeRepository = gradeRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllGrades()
        {
            // List<Grade> grades = await _gradeRepository.getAllAsync(["Student", "Course"]);

            List<Grade> grades = await _gradeRepository.getAllAsync(["Student", "Course"]);

            // _logger.LogInformation(grades[1].GradeName);
            var gradesDto = new List<GradeDto>();
            foreach (var grade in grades)
            {
                GradeDto gradeDto = grade.ToGradeDto();

                // var course = await _courseRepository.getByIdAsync(grade.CourseId);
                // var student = await _userRepository.getByIdAsync(grade.StudentId);
                // gradeDto.CourseName = course.Title;
                // gradeDto.StudentName = student.UserName;

                gradesDto.Add(gradeDto);
            }
            return Ok(gradesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGradeById(int id)
        {
            var grade = await _gradeRepository.findAsync(g => g.GradeId == id, ["Student", "Course"]);
            if (grade == null)
            {
                return NotFound("Grade not found");
            }
            // GradeDto gradeDto = grade.ToGradeDto();
            // if (grade.Student == null)
            // {
            //     var student = await _userRepository.getByIdAsync(grade.StudentId);
            //     gradeDto.StudentName = student.Name;
            // }
           
            return Ok(grade.ToGradeDto());
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGrade([FromBody] CreateGradeDto createGradeDto)
        {
            var course = await _courseRepository.getByIdAsync(createGradeDto.CourseId);
            if (course == null)
            {
                return NotFound("Course not found");
            }

            var student = await _userRepository.getByIdAsync(createGradeDto.StudentId);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            var grade = new Grade
            {
                Score = createGradeDto.Score,
                CourseId = createGradeDto.CourseId,
                StudentId = createGradeDto.StudentId,
                GradeName = createGradeDto.GradeName,
                dateTime = DateTime.Now,

            };

            await _gradeRepository.addAsync(grade);
            return Ok(grade.ToGradeDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(int id, [FromBody] UpdateGradeDto updateGradeDto)
        {
            if (id != updateGradeDto.GradeId)
            {
                return BadRequest("Grade ID mismatch");
            }

            var grade = await _gradeRepository.findAsync(g => g.GradeId == updateGradeDto.GradeId,["Student", "Course"]);
            if (grade == null)
            {
                return NotFound("Grade not found");
            }

            grade.Score = updateGradeDto.Score;
            _gradeRepository.update(grade);
            return Ok(grade.ToGradeDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await _gradeRepository.getByIdAsync(id);
            if (grade == null)
            {
                return NotFound("Grade not found");
            }

            _gradeRepository.Delete(grade);
            return Ok("Grade deleted successfully");
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetGradesByCourseId(int courseId)
        {
            var grades = await _gradeRepository.findAllAsync(g => g.CourseId == courseId, ["Student", "Course"]);
            // var grades = await _gradeRepository.findAllAsync(g => g.CourseId == courseId);


            return Ok(grades.ToGradsListDto());
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetGradesByStudentId(string studentId)
        {
            var grades = await _gradeRepository.findAllAsync(g => g.StudentId == studentId, ["Student", "Course"]);

            return Ok(grades.ToGradsListDto());
        }

        [HttpGet("student/{courseId}/{studentId}")]
        public async Task<IActionResult> GetStudentGradesByCourse(string studentId, int courseId)
        {
            var grades = await _gradeRepository.findAllAsync(g => g.StudentId == studentId && g.CourseId == courseId, ["Student", "Course"]);
            return Ok(grades.ToGradsListDto());
        }
    }
}
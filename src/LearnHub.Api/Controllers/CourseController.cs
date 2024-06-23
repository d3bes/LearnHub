using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearnHub.Core.Interfaces;
using LearnHub.Core.Models;
using LearnHub.Core.Dto;

namespace LearnHub.Api.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class CourseController : ControllerBase
   {
      private readonly IBaseRepository<Course> _courseRepository;
      private readonly IBaseRepository<User> _instructionRepository;


      public CourseController(IBaseRepository<Course> courseRepository, IBaseRepository<User> instructionRepository)
      {
         _courseRepository = courseRepository;
         _instructionRepository = instructionRepository;

      }

      [HttpGet("GetAllCourses")]
      public async Task<IActionResult> GetAllCourses()
      {

         var courses = await _courseRepository.getAllAsync(["Instructor"]);
         return Ok(courses);

      }

      [HttpGet("GetCourseById")]
      public async Task<IActionResult> GetCourseById(int id)
      {
         if (id != null)
         {
            var result = await _courseRepository.getByIdAsync(id);
            if (result != null)
            {
               return Ok(result);
            }
            else
               return NotFound();
         }
         return NoContent();
      }

      [HttpPost("AddCourse")]
      public async Task<IActionResult> AddCourse(Course course)
      {
         string instructorId = course.InstructorId;
         var instructor = await _instructionRepository.findAsync(x => x.Id == instructorId);
         course.Instructor = instructor;
         var result = await _courseRepository.AddAsync(course);
         return Ok(result);
      }

      [HttpPost("updateCourse")]
      public IActionResult UpdateCourse(CourseDto courseDto)
      {

         var course = _courseRepository.getById(courseDto.CourseId);
         course.Description = courseDto.Description;
         course.InstructorId = courseDto.InstructorId;
         course.Title = courseDto.Title;

         //var newInstructor = await _instructionRepository.findAsync(x=> x.Id == courseDto.InstructorId);
         var result = _courseRepository.update(course);

         return Ok(result);

      }

      [HttpDelete("deleteCourse")]
      public IActionResult DeleteCourse(int id)
      {
         var course = _courseRepository.getById(id);
         if (course != null)
         {
            _courseRepository.Delete(course);
            return Ok(true);
         }
         else
            return NotFound();

      }

   }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearnHub.Core.Interfaces;
using LearnHub.Core.Models;
using LearnHub.Core.Dto;
using LearnHub.Api.Extensions;

namespace LearnHub.Api.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class CourseController : ControllerBase
   {
      // private readonly IBaseRepository<Course> _courseRepository;
      // private readonly IBaseRepository<User> _instructorRepository;
      private readonly IUnitOfWork _unitOfWork;

      public CourseController(IUnitOfWork unitOfWork)//IBaseRepository<Course> courseRepository, IBaseRepository<User> instructorRepository)
      {
         // _courseRepository = courseRepository;
         // _instructorRepository = instructorRepository;
         _unitOfWork = unitOfWork;
      }

      [HttpGet("GetAllCourses")]
      public async Task<IActionResult> GetAllCourses()
      {

         // var courses = await _courseRepository.getAllAsync(["Instructor"]);
         var courses = await _unitOfWork.courses.getAllAsync(["Instructor"]);
         return Ok(courses.ToCourseListDto());

      }

      [HttpGet("GetCourseById")]
      public async Task<IActionResult> GetCourseById(int id)
      {
         if (id != null)
         {
            // Course result = await _courseRepository.findAsync(c=> c.CourseId == id ,["Instructor"]);
            Course result = await _unitOfWork.courses.findAsync(c => c.CourseId == id, ["Instructor"]);
            if (result != null)
            {
               return Ok(result.ToCourseDto());
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
         // var instructor = await _instructorRepository.findAsync(x => x.Id == instructorId);
         var instructor = await _unitOfWork.instructors.findAsync(x => x.Id == instructorId);

         course.Instructor = instructor;
         // var result = await _courseRepository.AddAsync(course);
         var result = await _unitOfWork.courses.AddAsync(course);
         _unitOfWork.Complete();
         return Ok(result.ToCourseDto());
      }

      [HttpPost("updateCourse")]
      public IActionResult UpdateCourse(CourseDto courseDto)
      {

         // var course = _courseRepository.getById(courseDto.CourseId);
         var course = _unitOfWork.courses.getById(courseDto.CourseId);
         course.Description = courseDto.Description;
         course.InstructorId = courseDto.InstructorId;
         course.Title = courseDto.Title;

         //var newInstructor = await _instructorRepository.findAsync(x=> x.Id == courseDto.InstructorId);
         // var result = _courseRepository.update(course);
         var result = _unitOfWork.courses.update(course);
         _unitOfWork.Complete();

         return Ok(result.ToCourseDto());

      }

      [HttpDelete("deleteCourse")]
      public IActionResult DeleteCourse(int id)
      {
         // var course = _courseRepository.getById(id);
         var course = _unitOfWork.courses.getById(id);

         if (course != null)
         {
            // _courseRepository.Delete(course);
            _unitOfWork.courses.Delete(course);
            _unitOfWork.Complete();

            return Ok(true);
         }
         else
            return NotFound();

      }

   }
}
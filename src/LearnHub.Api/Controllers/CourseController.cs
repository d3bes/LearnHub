using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearnHub.Core.Interfaces;
using LearnHub.Core.Models;

namespace LearnHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
     private readonly IBaseRepository<Course> _courseRepository;   

     public CourseController(IBaseRepository<Course> courseRepository)
     {
        _courseRepository = courseRepository;
     }

     [HttpGet]
     public IActionResult  GetAllCourses()
     {
        
        return Ok(_courseRepository.getAllAsync());

     }
    }
}
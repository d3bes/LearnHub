using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LearnHub.Core.Models;

namespace LearnHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IBaseRepository<Enrollment> _enrollmentRepository;
        public EnrollmentController(IBaseRepository<Enrollment> enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }
    }
}
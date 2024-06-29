using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Dto;
using LearnHub.Core.Interfaces;
using LearnHub.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LearnHub.Core.Services
{
    public class GradeService
    {


        private readonly IBaseRepository<Grade> _gradeRepository;
        private readonly IBaseRepository<Course> _courseRepository;
        private readonly IBaseRepository<IdentityUser> _userRepository;
        private readonly ILogger<GradeService> _logger;

        public GradeService(IBaseRepository<Grade> gradeRepository, IBaseRepository<Course> courseRepository
                                , IBaseRepository<IdentityUser> userRepository, ILogger<GradeService> logger)
        {
            _logger = logger;
            _gradeRepository = gradeRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }
 

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Models;

namespace LearnHub.EF.Repository
{
    public class ExtraEnrollmentRepo
    {
        private ApplicationDbContext _applicationDbContext;

        public ExtraEnrollmentRepo(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

  
    }
}
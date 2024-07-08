using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Models;

namespace LearnHub.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
            IBaseRepository<Course> courses { get; }
            IBaseRepository<Enrollment> enrollments { get; }
            IBaseRepository<Grade> grades { get; }
            IBaseRepository<Module> modules { get; }
            IBaseRepository<Lesson> lessons { get; }
            IBaseRepository<Content> contents { get; }

            IBaseRepository<User> users { get; }
            IBaseRepository<User> instructors { get; }


            int Complete();
        
    }
}
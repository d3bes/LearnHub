using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHub.Core.Interfaces;
using LearnHub.Core.Models;

namespace LearnHub.EF.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

            courses = new BaseRepository<Course>(_applicationDbContext);
            enrollments = new BaseRepository<Enrollment>(_applicationDbContext);
            grades = new BaseRepository<Grade>(_applicationDbContext);
            modules = new BaseRepository<Module>(_applicationDbContext);
            lessons = new BaseRepository<Lesson>(_applicationDbContext);
            contents = new BaseRepository<Content>(_applicationDbContext);
            users = new BaseRepository<User>(_applicationDbContext);
            instructors = new BaseRepository<User>(_applicationDbContext);


        }
        public IBaseRepository<Course> courses {get; private set; }

        public IBaseRepository<Enrollment> enrollments { get; private set; }
        public IBaseRepository<Grade> grades { get; private set; }

        public IBaseRepository<Module> modules { get; private set; }

        public IBaseRepository<Lesson> lessons { get; private set; }

        public IBaseRepository<Content> contents { get; private set; }

        public IBaseRepository<User> users  { get; private set; }

        public IBaseRepository<User> instructors  { get; private set; }

        public int Complete()
        {
            return _applicationDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}
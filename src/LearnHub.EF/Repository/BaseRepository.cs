using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LearnHub.Core;
using LearnHub.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.EF.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private ApplicationDbContext _applicationDbContext;
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            
        }

          public TEntity getById(int id)
        {
            return _applicationDbContext.Set<TEntity>().Find(id);

        }

        public async Task<TEntity> getByIdAsync(int id)
        {
            return await _applicationDbContext.Set<TEntity>().FindAsync(id);
        }

        public TEntity Add(TEntity entity)
        {
             _applicationDbContext.Set<TEntity>().Add(entity);
             _applicationDbContext.SaveChanges();
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
       await    _applicationDbContext.Set<TEntity>().AddAsync(entity);
             _applicationDbContext.SaveChanges();
            return entity;        }

     

        public List<TEntity> getAll()
        {
           return _applicationDbContext.Set<TEntity>().ToList();

        }

        public async Task<List<TEntity>> getAllAsync()
        {
           return await _applicationDbContext.Set<TEntity>().ToListAsync();
        }


        public TEntity find(Expression<Func<TEntity,bool>> expression)
        {

            return _applicationDbContext.Set<TEntity>().SingleOrDefault(expression);
        }

      

       
    }
}
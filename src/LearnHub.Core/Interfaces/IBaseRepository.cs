using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LearnHub.Core.Interfaces
{
    public interface IBaseRepository<TEntity>  where TEntity : class
    {

        TEntity getById(int  id );
        Task<TEntity> getByIdAsync(int id);

        Task<List<TEntity>> getAllAsync();
        List<TEntity> getAll();
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Add(TEntity entity);

        TEntity find(Expression<Func<TEntity,bool>> expression);

        // Task<TEntity> UpdateAsync(TEntity entity);
        
    }
}
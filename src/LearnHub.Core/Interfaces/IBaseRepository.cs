using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LearnHub.Core.Consts;

namespace LearnHub.Core.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Synchronise methodes
        /// </summary>
        TEntity getById(int id);

        List<TEntity> getAll();
        List<TEntity> getAll(string[] includs);

        TEntity Add(TEntity entity);

        TEntity find(Expression<Func<TEntity, bool>> expression);
        TEntity find(Expression<Func<TEntity, bool>> expression, string[] includs = null);



        IEnumerable<TEntity> findAll(Expression<Func<TEntity, bool>> expression);

        IEnumerable<TEntity> findAll(Expression<Func<TEntity, bool>> expression, string[] includs = null);
        IEnumerable<TEntity> findAll(Expression<Func<TEntity, bool>> expression, int take, int skip, string[] includs = null);

        IEnumerable<TEntity> findAll(Expression<Func<TEntity, bool>> criteria, int? take, int? skip,
            Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        TEntity add(TEntity entity);
        IEnumerable<TEntity> addRange(IEnumerable<TEntity> entities);

///#######################/
        TEntity update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        /// <summary>
        /// Asynchronise methodes
        /// </summary>

        Task<TEntity> getByIdAsync(int id);
        Task<List<TEntity>> getAllAsync();
        Task<List<TEntity>> getAllAsync(string[] includs);

        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> findAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> findAsync(Expression<Func<TEntity, bool>> expression, string[] includs = null);
        Task<IEnumerable<TEntity>> findAllAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> findAllAsync(Expression<Func<TEntity, bool>> expression, string[] includs = null);
        Task<IEnumerable<TEntity>> findAllAsync(Expression<Func<TEntity, bool>> expression, int take, int skip, string[] includs = null);

        Task<IEnumerable<TEntity>> findAllAsync(Expression<Func<TEntity, bool>> criteria, int? skip, int? take,
            Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);


        Task<TEntity> addAsync(TEntity entity);

        Task<IEnumerable<TEntity>> addRangeAsync(IEnumerable<TEntity> entities);

        // Task<TEntity> UpdateAsync(TEntity entity);

    }

}
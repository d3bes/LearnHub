using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LearnHub.Core;
using LearnHub.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using LearnHub.Core.Consts;

namespace LearnHub.EF.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private ApplicationDbContext _applicationDbContext;
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

        /// <summary>
        /// Synchronise methodes
        /// </summary>
        public TEntity getById(int id)
        {
            return _applicationDbContext.Set<TEntity>().Find(id);

        }

        public TEntity Add(TEntity entity)
        {
            _applicationDbContext.Set<TEntity>().Add(entity);
            _applicationDbContext.SaveChanges();
            return entity;
        }


        public List<TEntity> getAll()
        {
            return _applicationDbContext.Set<TEntity>().ToList();

        }
        public TEntity find(Expression<Func<TEntity, bool>> expression)
        {


            return _applicationDbContext.Set<TEntity>().SingleOrDefault(expression);
        }


        public TEntity find(Expression<Func<TEntity, bool>> expression, string[] includs)
        {
            IQueryable<TEntity> query = _applicationDbContext.Set<TEntity>();
            if (includs != null)
            {
                foreach (var item in includs)
                {
                    query.Include(item);
                }

            }

            return query.SingleOrDefault(expression);

        }
        public IEnumerable<TEntity> findAll(Expression<Func<TEntity, bool>> expression)
        {
            return _applicationDbContext.Set<TEntity>().Where(expression).ToList();

        }


        public IEnumerable<TEntity> findAll(Expression<Func<TEntity, bool>> expression, string[] includs = null)
        {

            IQueryable<TEntity> query = _applicationDbContext.Set<TEntity>();
            if (includs != null)
            {
                foreach (var item in includs)
                {
                    query.Include(item);
                }

            }

            return query.Where(expression).ToList();
        }
        public IEnumerable<TEntity> findAll(Expression<Func<TEntity, bool>> expression, int take, int skip, string[] includs = null)
        {

            IQueryable<TEntity> query = _applicationDbContext.Set<TEntity>();
            if (includs != null)
            {
                foreach (var item in includs)
                {
                    query.Include(item);
                }

            }

            return query.Where(expression).Take(take).Skip(skip).ToList();
        }
        public IEnumerable<TEntity> findAll(Expression<Func<TEntity, bool>> criteria, int? take, int? skip,
            Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<TEntity> query = _applicationDbContext.Set<TEntity>().Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.ToList();
        }

        public TEntity add(TEntity entity)
        {
            _applicationDbContext.Set<TEntity>().Add(entity);
            _applicationDbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> addRange(IEnumerable<TEntity> entities)
        {
            _applicationDbContext.Set<TEntity>().AddRange(entities);
            _applicationDbContext.SaveChanges();
            return entities.ToList();

        }

        public TEntity update(TEntity entity)
        {
            _applicationDbContext.Set<TEntity>().Update(entity);
            _applicationDbContext.SaveChanges();
            return entity;
        }
        public void Delete(TEntity entity)
        {
            _applicationDbContext.Set<TEntity>().Remove(entity);
            _applicationDbContext.SaveChanges();
        }
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _applicationDbContext.Set<TEntity>().RemoveRange(entities);
            _applicationDbContext.SaveChanges();
        }



        /// <summary>
        /// Asynchronise methodes
        /// </summary>


        public async Task<TEntity> getByIdAsync(int id)
        {
            return await _applicationDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> getAllAsync()
        {
            return await _applicationDbContext.Set<TEntity>().ToListAsync();
        }



        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _applicationDbContext.Set<TEntity>().AddAsync(entity);
            _applicationDbContext.SaveChanges();
            return entity;
        }


        public async Task<TEntity> findAsync(Expression<Func<TEntity, bool>> expression)
        {


            return await _applicationDbContext.Set<TEntity>().SingleOrDefaultAsync(expression);
        }


        public async Task<TEntity> findAsync(Expression<Func<TEntity, bool>> expression, string[] includs)
        {
            IQueryable<TEntity> query = _applicationDbContext.Set<TEntity>();
            if (includs != null)
            {
                foreach (var item in includs)
                {
                    query.Include(item);
                }

            }

            return await query.SingleOrDefaultAsync(expression);

        }
        public async Task<IEnumerable<TEntity>> findAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _applicationDbContext.Set<TEntity>().Where(expression).ToListAsync();

        }



        public async Task<IEnumerable<TEntity>> findAllAsync(Expression<Func<TEntity, bool>> expression, string[] includs = null)
        {

            IQueryable<TEntity> query = _applicationDbContext.Set<TEntity>();
            if (includs != null)
            {
                foreach (var item in includs)
                {
                    query.Include(item);
                }

            }

            return await query.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> findAllAsync(Expression<Func<TEntity, bool>> expression, int take, int skip, string[] includs = null)
        {

            IQueryable<TEntity> query = _applicationDbContext.Set<TEntity>();
            if (includs != null)
            {
                foreach (var item in includs)
                {
                    query.Include(item);
                }

            }

            return await query.Where(expression).Take(take).Skip(skip).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> findAllAsync(Expression<Func<TEntity, bool>> criteria, int? take, int? skip,
           Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<TEntity> query = _applicationDbContext.Set<TEntity>().Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }


        public async Task<TEntity> addAsync(TEntity entity)
        {
            await _applicationDbContext.Set<TEntity>().AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> addRangeAsync(IEnumerable<TEntity> entities)
        {
            await _applicationDbContext.Set<TEntity>().AddRangeAsync(entities);
            await _applicationDbContext.SaveChangesAsync();
            return entities.ToList();

        }


    }

}
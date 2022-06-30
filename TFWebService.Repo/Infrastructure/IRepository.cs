using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TFWebService.Repo.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Inser(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        void Delete(Expression<Func<TEntity,bool>> where);


        IEnumerable<TEntity> GetAll();
        TEntity GetById(object id);
        IEnumerable<TEntity> GetMany(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includEntity);
        TEntity Get(Expression<Func<TEntity, bool>> where);


        //------------------------------------------------------------------------

        Task InserAsync(TEntity entity);


        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetManyAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includEntity);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where,
            string includEntity = "");
    }
}

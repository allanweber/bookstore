using Bookstore.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bookstore.Framework.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> GetAsync(params object[] keys);

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);

        Task<long> CountAsync();

        Task<long> CountAsync(Expression<Func<TEntity, bool>> expression);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task CommitAsync();
    }
}

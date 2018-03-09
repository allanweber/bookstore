using Bookstore.Framework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bookstore.Framework.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(PrincipalDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        public virtual Task CommitAsync()
        {
            return this.dbContext.SaveChangesAsync();
        }

        public virtual Task<long> CountAsync()
        {
            return this.Query().LongCountAsync();
        }

        public virtual Task<long> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return this.Query().LongCountAsync(expression);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Task.Run(() => this.dbSet.Remove(entity));
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return this.Query().AnyAsync(expression);
        }

        public virtual Task<TEntity> GetAsync(params object[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));

            return this.dbSet.FindAsync(keys);
        }

        public virtual Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return this.dbSet.AddAsync(entity);
        }

        public virtual Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return this.Query().Where(expression).ToListAsync();
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Task.Run(() => this.dbSet.Update(entity));
        }

        protected IQueryable<TEntity> QueryAsTracking()
        {
            return this.dbSet;
        }

        protected IQueryable<TEntity> Query()
        {
            return this.dbSet.AsNoTracking();
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return this.Query().ToListAsync();
        }
    }
}

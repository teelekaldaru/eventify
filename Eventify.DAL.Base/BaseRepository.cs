using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eventify.Common.Utils.Exceptions;

namespace Eventify.DAL.Base
{
    public abstract class BaseRepository<TContext> where TContext : DbContext
    {
	    private DbContextOptions Options { get; }

        protected BaseRepository(DbContextOptions options)
        {
            Options = options;
        }

        protected async Task<IList<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            await using var context = CreateContext();
            return await context.Set<TEntity>().ToListAsync();
        }

        protected async Task<TEntity?> GetAsync<TEntity>(params object[] id) where TEntity : class
        {
            await using var context = CreateContext();
            return await context.Set<TEntity>().FindAsync(id);
        }

        protected async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await using var context = CreateContext();
            var added = await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return added.Entity;
        }

        protected async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await using var context = CreateContext();
            var updated = context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return updated.Entity;
        }

        protected async Task<TEntity> RemoveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await using var context = CreateContext();
            var removed = context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
            return removed.Entity;
        }

        protected TContext CreateContext()
        {
            var obj = (TContext)Activator.CreateInstance(typeof(TContext), Options) ?? throw new SimpleException("DbContext can't be open");
            obj.Database.SetCommandTimeout(3600);
            return obj;
        }
    }
}

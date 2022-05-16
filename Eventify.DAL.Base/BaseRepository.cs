using Eventify.Common.Classes.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventify.DAL.Base
{
    public abstract class BaseRepository<TContext> where TContext : DbContext
    {
        protected const int Timeout = 3600;

        private DbContextOptions Options { get; }

        protected BaseRepository(DbContextOptions options)
        {
            Options = options;
        }

        protected async Task<IList<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            await using TContext context = CreateContext();
            return await context.Set<TEntity>().ToListAsync();
        }

        protected async Task<TEntity> GetAsync<TEntity>(params object[] id) where TEntity : class
        {
            await using TContext context = CreateContext();
            return await context.Set<TEntity>().FindAsync(id);
        }

        protected async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await using TContext context = CreateContext();
            EntityEntry<TEntity> added = await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return added.Entity;
        }

        protected async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await using TContext context = CreateContext();
            EntityEntry<TEntity> updated = context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return updated.Entity;
        }

        protected async Task<TEntity> RemoveAsync<TEntity>(params object[] id) where TEntity : class
        {
            await using TContext context = CreateContext();
            TEntity entity = context.Set<TEntity>().Find(id);
            return await RemoveAsync(entity);
        }

        protected async Task<TEntity> RemoveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await using TContext context = CreateContext();
            EntityEntry<TEntity> removed = context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
            return removed.Entity;
        }

        protected TContext CreateContext()
        {
            TContext obj = (TContext)Activator.CreateInstance(typeof(TContext), Options) ?? throw new SimpleException("DbContext can't be open");
            obj.Database.SetCommandTimeout(3600);
            return obj;
        }
    }
}

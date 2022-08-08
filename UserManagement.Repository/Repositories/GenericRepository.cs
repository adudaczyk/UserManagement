using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UserManagement.EntityFramework.Interfaces;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Repository.Repositories
{
    public abstract class GenericRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        protected readonly TContext context;

        public GenericRepository(TContext context)
        {
            this.context = context;
        }

        public virtual TEntity Add(TEntity entity)
        {
            return context.Add(entity).Entity;
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
        }

        public virtual TEntity Get(int id)
        {
            return context.Find<TEntity>(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().AsQueryable().Where(predicate).ToList();
        }

        public virtual TEntity Update(TEntity entity)
        {
            return context.Update(entity).Entity;
        }

        public virtual void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entity)
        {
            context.Set<TEntity>().RemoveRange(entity);
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}

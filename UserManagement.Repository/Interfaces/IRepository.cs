using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UserManagement.EntityFramework.Interfaces;

namespace UserManagement.Repository.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        T Add(T entity);
        T Update(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
        void SaveChanges();
    }
}

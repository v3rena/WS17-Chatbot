using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Chatbot.DataAccessLayer.Interfaces
{
    public interface IRepository<T> : IBaseRepository where T : IEntity
    {
        void Create(T entity);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> condition);

        IEnumerable<T> Read(Expression<Func<T, bool>> condition);

        void Update(T entity);

        void Save(T entity);
    }
}
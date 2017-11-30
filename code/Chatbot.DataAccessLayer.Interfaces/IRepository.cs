using System;
using System.Collections.Generic;

namespace Chatbot.DataAccessLayer.Interfaces
{
    public interface IRepository<T> : IBaseRepository where T : IEntity
    {
        void Create(T entity);

        void Delete(T entity);

        void Delete(Func<T, bool> condition);

        IEnumerable<T> Read(Func<T, bool> condition);

        void Update(T entity);

        void Save(T entity);
    }
}
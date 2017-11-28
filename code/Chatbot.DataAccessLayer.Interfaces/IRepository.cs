using System;
using System.Collections.Generic;

namespace Chatbot.DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        void Create(T message);

        void Delete(T message);

        void Delete(Func<T, bool> condition);

        IEnumerable<T> Read(Func<T, bool> condition);

        void Update(T message);
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Chatbot.DataAccessLayer.Interfaces
{
    public abstract class Repository<T, U> : IRepository<T> where T : IEntity where U: DbContext
    {
        protected readonly U dbContext;

        public Repository(U dbContext)
        {
            this.dbContext = dbContext;
        }

        public abstract void Create(T message);

        public abstract IEnumerable<T> Read(Func<T, bool> condition);

        public abstract void Update(T message);

        public abstract void Delete(T message);

        public abstract void Delete(Func<T, bool> condition);
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Chatbot.DataAccessLayer.Interfaces
{
    public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : class, IEntity where TDbContext: DbContext
    {
        protected readonly TDbContext dbContext;

        public Repository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public abstract void Create(TEntity entity);

        public abstract IEnumerable<TEntity> Read(Func<TEntity, bool> condition);

        public abstract void Update(TEntity entity);

        public abstract void Delete(TEntity entity);

        public abstract void Delete(Func<TEntity, bool> condition);

        public void Save(TEntity entity)
        {
            if (entity.Id == 0)
            {
                Create(entity);
            } else
            {
                Update(entity);
            }
        }
    }
}
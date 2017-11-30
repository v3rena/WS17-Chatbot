using Chatbot.DataAccessLayer.Context;
using Chatbot.DataAccessLayer.Entities;
using Chatbot.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.DataAccessLayer.Repositories
{
    public class SessionKeyRepository : Repository<SessionKey, ChatbotContext>
    {
        public SessionKeyRepository(ChatbotContext dbContext) : base(dbContext)
        {

        }

        public override void Create(SessionKey entity)
        {
            dbContext.SessionKeys.Add(entity);
            dbContext.SaveChanges();
        }

        public override IEnumerable<SessionKey> Read(Func<SessionKey, bool> condition)
        {
            return dbContext.SessionKeys.Where(condition);
        }

        public override void Update(SessionKey entity)
        {
            dbContext.SessionKeys.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public override void Delete(SessionKey entity)
        {
            dbContext.SessionKeys.Remove(entity);
            dbContext.SaveChanges();
        }

        public override void Delete(Func<SessionKey, bool> condition)
        {
            dbContext.SessionKeys.RemoveRange(dbContext.SessionKeys.Where(condition));
            dbContext.SaveChanges();
        }
    }
}

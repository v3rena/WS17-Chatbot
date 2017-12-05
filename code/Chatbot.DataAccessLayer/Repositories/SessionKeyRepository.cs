using Chatbot.DataAccessLayer.Context;
using Chatbot.DataAccessLayer.Entities;
using Chatbot.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.DataAccessLayer.Repositories
{
    public class SessionKeyRepository : Repository<SessionKey, ChatbotContext>
    {
        public SessionKeyRepository(ChatbotContext dbContext) : base(dbContext)
        {

        }

        public override void Create(SessionKey sessionKey)
        {
            dbContext.SessionKeys.Add(sessionKey);
            dbContext.SaveChanges();
        }

        public override IEnumerable<SessionKey> Read(Expression<Func<SessionKey, bool>> condition)
        {
            return dbContext.SessionKeys.Where(condition);
        }

        public override void Update(SessionKey sessionKey)
        {
            var original = dbContext.ChangeTracker.Entries<SessionKey>().Single(i => i.Entity.Id == sessionKey.Id);
            original.CurrentValues.SetValues(sessionKey);
            dbContext.SaveChanges();
        }

        public override void Delete(SessionKey sessionKey)
        {
            dbContext.SessionKeys.Remove(sessionKey);
            dbContext.SaveChanges();
        }

        public override void Delete(Expression<Func<SessionKey, bool>> condition)
        {
            dbContext.SessionKeys.RemoveRange(dbContext.SessionKeys.Where(condition));
            dbContext.SaveChanges();
        }
    }
}

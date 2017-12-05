using Chatbot.DataAccessLayer.Context;
using Chatbot.DataAccessLayer.Entities;
using Chatbot.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Chatbot.DataAccessLayer.Repositories
{
    public class MessageRepository : Repository<Message, ChatbotContext>
    {
        public MessageRepository(ChatbotContext dbContext) : base (dbContext)
        {

        }

        public override void Create(Message message)
        {
            dbContext.Messages.Add(message);
            dbContext.SaveChanges();
        }

        public override IEnumerable<Message> Read(Expression<Func<Message, bool>> condition)
        {
            return dbContext.Messages.Where(condition);
        }

        public override void Update(Message message)
        {
            var original = dbContext.ChangeTracker.Entries<Message>().Single(i => i.Entity.Id == message.Id);
            original.CurrentValues.SetValues(message);
            dbContext.SaveChanges();
        }

        public override void Delete(Message message)
        {
            dbContext.Messages.Remove(message);
            dbContext.SaveChanges();
        }

        public override void Delete(Expression<Func<Message, bool>> condition)
        {
            dbContext.Messages.RemoveRange(dbContext.Messages.Where(condition));
            dbContext.SaveChanges();
        }
    }
}

using Chatbot.DataAccessLayer.Context;
using Chatbot.DataAccessLayer.Entities;
using Chatbot.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public override IEnumerable<Message> Read(Func<Message, bool> condition)
        {
            return dbContext.Messages.Where(condition).ToList();
        }

        public override void Update(Message message)
        {
            dbContext.Messages.Attach(message);
            dbContext.Entry(message).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public override void Delete(Message message)
        {
            dbContext.Messages.Remove(message);
            dbContext.SaveChanges();
        }

        public override void Delete(Func<Message, bool> condition)
        {
            dbContext.Messages.RemoveRange(dbContext.Messages.Where(condition).ToList());
        }
    }
}

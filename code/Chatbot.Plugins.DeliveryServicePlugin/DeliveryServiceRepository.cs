using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Chatbot.DataAccessLayer.Interfaces;
using Chatbot.Plugins.DeliveryService.Models;
using System.Linq;

namespace Chatbot.Plugins.DeliveryServicePlugin
{
    public class DeliveryServiceRepository : Repository<Order, DeliveryServiceContext>
    {
        public DeliveryServiceRepository(DeliveryServiceContext dbContext) : base(dbContext)
        {

        }

        public override void Create(Order order)
        {
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

        }

        public override IEnumerable<Order> Read(Expression<Func<Order, bool>> condition)
        {
            return dbContext.Orders.Where(condition);
        }

        public override void Update(Order order)
        {
            var original = dbContext.ChangeTracker.Entries<Order>().Single(i => i.Entity.Id == order.Id);
            original.CurrentValues.SetValues(order);
            dbContext.SaveChanges();
        }

        public override void Delete(Order order)
        {
            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
        }

        public override void Delete(Expression<Func<Order, bool>> condition)
        {
            dbContext.Orders.RemoveRange(dbContext.Orders.Where(condition));
            dbContext.SaveChanges();
        }
    }
}

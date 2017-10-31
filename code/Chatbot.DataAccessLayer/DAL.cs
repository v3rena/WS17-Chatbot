using Autofac;
using Chatbot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Chatbot.Interfaces;
using Chatbot.DataAccessLayer.Context;

namespace Chatbot.DataAccessLayer
{
    public class DAL : IDataAccessLayer
    {
        public DAL()
        {
            // TODO: Add after migration
            Database.SetInitializer<MessageContext>(new MigrateDatabaseToLatestVersion<MessageContext, Migrations.ConfigurationMessage>());
            using (var context = new MessageContext())
            {
                context.Database.Initialize(false);
            }

        }

        public string Name => GetName();

        public string GetName()
        {
            return "DataAccessLayer";
        }

        public string GetTest(int id)
        {
            Message result = null;
            using (var context = new MessageContext())
            {
                //result = context.Message.FirstOrDefault();
            }
            return result?.Content;
        }

        public int Insert(Message item)
        {
            using (var context = new MessageContext())
            {
                context.Message.Add(item);
                return context.SaveChanges();
            }
        }

        public IEnumerable<Message> Select(Func<Test, bool> condition)
        {
            IEnumerable<Message> result = null;
            using (var context = new MessageContext())
            {
                result = context
                    .Message
                    //.Where(condition)
                    .ToList();
            }
            return result;
        }
        /*
        public Test SelectFirst(Func<Test, bool> condition)
        {
            return Select(condition).FirstOrDefault();
        }*/

        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                //base.Load(builder);
                builder
                    .Register(component => new DAL())
                    .As<IDataAccessLayer>()
                    .InstancePerLifetimeScope();

                /*
                 builder.Register(c => new ValuesService(c.Resolve<ILogger<ValuesService>>()))
                        .As<IValuesService>()
                    .InstancePerLifetimeScope();
                 */
            }
        }

    }
}
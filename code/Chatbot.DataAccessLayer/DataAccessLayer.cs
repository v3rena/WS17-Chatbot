using Autofac;
using Chatbot.Common.Interfaces;
using Chatbot.DataAccessLayer.Context;
using Chatbot.DataAccessLayer.Entities;
using Chatbot.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Chatbot.DataAccessLayer
{
    public class DataAccessLayer : IDataAccessLayer
    {
        public DataAccessLayer()
        {
            // TODO: Add after migration
            Database.SetInitializer<ChatbotContext>(new MigrateDatabaseToLatestVersion<ChatbotContext, Migrations.ConfigurationMessage>());
            using (var context = new ChatbotContext())
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
            using (var context = new ChatbotContext())
            {
                //result = context.Message.FirstOrDefault();
            }
            return result?.Content;
        }

        public int Insert(Message item)
        {
            using (var context = new ChatbotContext())
            {
                context.Messages.Add(item);
                return context.SaveChanges();
            }
        }

        public IEnumerable<Message> Select(Func<Message, bool> condition)
        {
            IEnumerable<Message> result = null;
            using (var context = new ChatbotContext())
            {
                result = context
                    .Messages
                    //.Where(condition)
                    .ToList();
            }
            return result;
        }

        public Dictionary<string, string> GetPluginConfiguration(IPlugin plugin)
        {
            using (var context = new ChatbotContext())
            {
                return context.PluginConfigurations
                    .Where(i => i.Name == plugin.Name)
                    .Select(i => new { i.Key, i.Value })
                    //.AsEnumerable()
                    .ToDictionary(i => i.Key, i => i.Value);
            }
        }

        public void SavePluginConfiguration(IPlugin plugin, Dictionary<string, string> configuration)
        {
            using (var context = new ChatbotContext())
            {
                IEnumerable<PluginConfiguration> newConfig = configuration.Select(i => new PluginConfiguration() { Name = plugin.Name, Key = i.Key, Value = i.Value });
                context.PluginConfigurations.RemoveRange(context.PluginConfigurations.Where(i => i.Name == plugin.Name));
                context.PluginConfigurations.AddRange(newConfig);
                context.SaveChanges();
            }
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
                    .Register(component => new DataAccessLayer())
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
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
    public class PluginConfigurationRepository : Repository<PluginConfiguration, ChatbotContext>
    {
        public PluginConfigurationRepository(ChatbotContext dbContext) : base(dbContext)
        {

        }

        public override void Create(PluginConfiguration pluginConfiguration)
        {
            dbContext.PluginConfigurations.Add(pluginConfiguration);
            dbContext.SaveChanges();
        }
        public override IEnumerable<PluginConfiguration> Read(Expression<Func<PluginConfiguration, bool>> condition)
        {
            return dbContext.PluginConfigurations.Where(condition);
        }

        public override void Update(PluginConfiguration pluginConfiguration)
        {
            var original = dbContext.ChangeTracker.Entries<PluginConfiguration>().Single(i => i.Entity.Id == pluginConfiguration.Id);
            original.CurrentValues.SetValues(pluginConfiguration);
            dbContext.SaveChanges();
        }

        public override void Delete(PluginConfiguration pluginConfiguration)
        {
            dbContext.PluginConfigurations.Remove(pluginConfiguration);
            dbContext.SaveChanges();
        }

        public override void Delete(Expression<Func<PluginConfiguration, bool>> condition)
        {
            dbContext.PluginConfigurations.RemoveRange(dbContext.PluginConfigurations.Where(condition));
            dbContext.SaveChanges();
        }
    }
}

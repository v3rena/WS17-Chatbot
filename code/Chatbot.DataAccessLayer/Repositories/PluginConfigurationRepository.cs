using Chatbot.DataAccessLayer.Context;
using Chatbot.DataAccessLayer.Entities;
using Chatbot.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
        public override IEnumerable<PluginConfiguration> Read(Func<PluginConfiguration, bool> condition)
        {
            return dbContext.PluginConfigurations.Where(condition).ToList();
        }

        public override void Update(PluginConfiguration pluginConfiguration)
        {
            dbContext.PluginConfigurations.Attach(pluginConfiguration);
            dbContext.Entry(pluginConfiguration).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public override void Delete(PluginConfiguration pluginConfiguration)
        {
            dbContext.PluginConfigurations.Remove(pluginConfiguration);
            dbContext.SaveChanges();
        }

        public override void Delete(Func<PluginConfiguration, bool> condition)
        {
            dbContext.PluginConfigurations.RemoveRange(dbContext.PluginConfigurations.Where(condition).ToList());
            dbContext.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Chatbot.Common.Interfaces;
using Chatbot.BusinessLayer.Models;

namespace Chatbot.Plugins.EchoBot
{
    public class EchoBot : IPlugin
    {
        public string Name => "EchoBot";

        public float CanHandle(Message message)
        {
            return 0.2f;
        }

        public IEnumerable<PluginConfiguration> EnsureDefaultConfiguration(IEnumerable<PluginConfiguration> configuration)
        {
            //No configuration necessary
            return configuration;
        }

        public Message Handle(Message message)
        {
            Database.SetInitializer<EchoBotContext>(new MigrateDatabaseToLatestVersion<EchoBotContext, Migrations.Configuration>());
            return new Message("Echo: " + message.Content);
        }
    }
}
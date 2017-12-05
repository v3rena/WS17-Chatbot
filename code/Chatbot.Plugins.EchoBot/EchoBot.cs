using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;

namespace Chatbot.Plugins.EchoBot
{
    public class EchoBot : IPlugin
    {
        public string Name => "EchoBot";

        public float CanHandle(Message message)
        {
            return 0.2f;
        }

        public IDictionary<string, string> EnsureDefaultConfiguration(IDictionary<string, string> configuration)
        {
            //No configuration necessary
            return configuration;
        }

        public void RefreshConfiguration(IDictionary<string, string> configuration)
        {
            //No configuration necessary
        }

        public Message Handle(Message message)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EchoBotContext, Migrations.Configuration>());
            return new Message("Echo: " + message.Content);
        }
    }
}
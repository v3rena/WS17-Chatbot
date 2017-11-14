using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chatbot.Interfaces;
using Chatbot.Models;
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

        public Dictionary<string, string> EnsureDefaultConfiguration(Dictionary<string, string> configuration)
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
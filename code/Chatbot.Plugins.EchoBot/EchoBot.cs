using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chatbot.Interfaces;
using Chatbot.Interfaces.Models;
using Chatbot.Models;
using System.Data.Entity;

namespace Chatbot.Plugins.EchoBot
{
    public class EchoBot : IPlugin
    {
        public string Name => "EchoBot";

        public float CanHandle(IMessage message)
        {
            return 0.2f;
        }

       
        public IMessage Handle(IMessage message)
        {
            Database.SetInitializer<EchoBotContext>(new MigrateDatabaseToLatestVersion<EchoBotContext, Migrations.Configuration>());
            return new Message("Echo: " + message.Content);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chatbot.Interfaces;
using Chatbot.Models;

namespace Chatbot.Plugins.EchoBot
{
    public class EchoBot : IPlugin
    {
        public string Name => "EchoBot";

        public float CanHandle(Message message)
        {
            return 0.2f;
        }

       
        public Message Handle(Message message)
        {
            return new Message("Echo: " + message.Content);
        }
    }
}
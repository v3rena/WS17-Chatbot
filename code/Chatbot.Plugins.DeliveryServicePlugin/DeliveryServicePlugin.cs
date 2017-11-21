using Chatbot.Interfaces;
using Chatbot.Models;
using System;
using System.Collections.Generic;

namespace Chatbot.Plugins.DeliveryServicePlugin
{
    public class DeliveryServicePlugin : IPlugin
    {
        public string Name => "DeliveryServicePlugin";

        public float CanHandle(Message message)
        {
            if (message.Content.Contains("order something"))
            {
                return 1.0f;
            }
            return 0.0f;
        }

        public Dictionary<string, string> EnsureDefaultConfiguration(Dictionary<string, string> configuration)
        {
            return configuration;
        }

        public Message Handle(Message message)
        {
            Message response = new Message();
            response.Content = "DeliveryService Plugin TRIGGERED!\n\n*autistic screeching*REEEEEEEEEEEE!!!!";
            response.SessionKey = message.SessionKey;
            return response;
        }
    }
}

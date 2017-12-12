using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using System.Collections.Generic;

namespace Chatbot.Plugins.DeliveryService
{
    public class DeliveryService : IPlugin
    {
        public string Name => "DeliveryService";

        public float CanHandle(Message message)
        {
            if (message.Content.Contains("order something"))
            {
                return 1.0f;
            }
            return 0.0f;
        }

        public IDictionary<string, string> EnsureDefaultConfiguration(IDictionary<string, string> configuration)
        {
            return configuration;
        }
    
        public void RefreshConfiguration(IDictionary<string, string> configuration)
        {
            
        }

        public Message Handle(Message message)
        {
            Message response = new Message
            {
                Content = "DeliveryService Plugin TRIGGERED!\n\n*autistic screeching*REEEEEEEEEEEE!!!!",
                SessionKey = message.SessionKey
            };
            return response;
        }
    }
}

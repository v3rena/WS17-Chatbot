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

        public IEnumerable<PluginConfiguration> EnsureDefaultConfiguration(IList<PluginConfiguration> configuration)
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

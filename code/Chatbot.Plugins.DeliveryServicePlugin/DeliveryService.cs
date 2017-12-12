using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using Chatbot.Plugins.DeliveryService.Models;
using Chatbot.Plugins.DeliveryServicePlugin;
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
            OrderAddress orderAddress = new OrderAddress("Michael Palata", "Musteralee 1/5", "2130 Mistelbach", "0664 123 456 789");

            OrderWrapper order = new OrderWrapper(orderAddress);
            order.Positions.Add(new OrderPosition("Pizza Salami", 1, "extra Mais und doppelt Käse"));
            order.Positions.Add(new OrderPosition("Pizza Provenciale", 1));
            order.Positions.Add(new OrderPosition("Pizza Mageritha", 1, "mit Mais"));

            Order entity = new Order();
            entity.SetOrder(order);

            DeliveryServiceContext context = new DeliveryServiceContext();
            DeliveryServiceRepository dal = new DeliveryServiceRepository(context);

            dal.Create(entity);

            Message response = new Message();
            response.Content = "DeliveryService Plugin TRIGGERED!\n*autistic screeching*REEEEEEEEEEEE!!!!\n++++++++++++++++++++++++++++++\n\n" + entity.OrderJsonData;
            response.SessionKey = message.SessionKey;
            return response;
        }
    }
}

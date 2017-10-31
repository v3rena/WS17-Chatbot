using Chatbot.Interfaces;
using Chatbot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.RoutePlannerBot
{
    public class RoutePlannerBot : IPlugin
    {
        public string Name => throw new NotImplementedException();

        public float CanHandle(Interfaces.Models.IMessage message)
        {
            float handle = 0.0f;
            if(contains(message, "route"))
            {
                handle += 0.4f;
            }
            if (contains(message, "von") && contains(message, "nach"))
            {
                handle += 0.5f;
            }
            return handle;
        }

        public bool contains(Interfaces.Models.IMessage message, string str2)
        {
            return message.Content.ToLower().Contains(str2);
        }

        public Interfaces.Models.IMessage Handle(Interfaces.Models.IMessage message)
        {
            string messageStr = "error";
            string[] parts = message.Content.Split(new string[] { "von", "nach" }, StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length >= 2)
            {
                DirectionsRequest request = new DirectionsRequest(parts[parts.Length - 2], parts[parts.Length - 1]);
                string jsonResponse = request.GetResponse();
                messageStr = DirectionsResponseConverter.GetHumanReadableDirections(jsonResponse);
            }
            return new Message(messageStr);
        }
    }
}

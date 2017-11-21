using Chatbot.Interfaces;
using Chatbot.Models;
using System;
using System.Collections.Generic;

namespace Chatbot.Plugins.RoutePlannerBot
{
    public class RoutePlannerBot : IPlugin
    {

        private string apiKey = "AIzaSyA-lYBx2cblAh7I4tID_Db2lornpVyjNWU";
        public string Name => "RoutePlannerBot";

        public float CanHandle(Message message)
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

        public bool contains(Message message, string str2)
        {
            return message.Content.ToLower().Contains(str2);
        }

        public Message Handle(Message message)
        {
            string messageStr = "error";
            string[] parts = message.Content.Split(new string[] { "von", "nach" }, StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length >= 2)
            {
                DirectionsRequest request = new DirectionsRequest(parts[parts.Length - 2], parts[parts.Length - 1], apiKey);
                string jsonResponse = request.GetResponse();
                messageStr = DirectionsResponseConverter.GetHumanReadableDirections(jsonResponse);
            }
            return new Message(messageStr);
        }

        public Dictionary<string, string> EnsureDefaultConfiguration(Dictionary<string, string> configuration)
        {
            if(configuration.ContainsKey("apiKey"))
            {
                this.apiKey = configuration["apiKey"];
            }
            return configuration;
        }
    }
}

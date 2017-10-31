using Chatbot.Interfaces;
using Chatbot.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.WeatherPlugin
{
    public class WeatherPlugin : IPlugin
    {
        public string Name => "WeatherPlugin";

        private static List<string> stringLibrary = new List<string> { "wetter", "temperatur", "regen", "sonne", "wolken" };
        public string Location { get; set; }

        public float CanHandle(Message message)
        {
            message.Content.ToLower();

            if (stringLibrary.Any(s => message.Content.Contains(s)))
                return 1;
            else
                return 0;   
        }

        public Message Handle(Message message)
        {
            //string apiKey = ConfigurationManager.AppSettings["OpenWeatherMapAPIKey"];
            throw new NotImplementedException();
        }
    }
}

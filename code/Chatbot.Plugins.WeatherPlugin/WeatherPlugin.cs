using Chatbot.Interfaces;
using Chatbot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.WeatherPlugin
{
    public class WeatherPlugin : IPlugin
    {
        public string Name => "WeatherPlugin";

        public float CanHandle(Message message)
        {
            throw new NotImplementedException();
        }

        public Message Handle(Message message)
        {
            throw new NotImplementedException();
        }
    }
}

using Chatbot.BusinessLayer.Models;
using Chatbot.Plugins.WeatherPlugin.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.WeatherPlugin
{
    static class ResponseMessage
    {
        public static Message Ok(List<ICommand> commands, WeatherInformation weatherInformation)
        {
            StringBuilder stringBuilder = new StringBuilder($"Wetter in {weatherInformation.CityName}:\n");

            commands.ForEach(rs => stringBuilder.Append($"{rs.GetInformation(weatherInformation)}"));

            return new Message(stringBuilder.ToString());
        }

        public static Message CityNotFoundMessage(string city)
        {
            return new Message($"'{city}' nicht gefunden!")
            {
                IsUserMessage = false
            };
        }
    }
}

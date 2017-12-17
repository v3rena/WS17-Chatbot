using Chatbot.BusinessLayer.Models;
using Chatbot.Plugins.WeatherPlugin.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatbot.Plugins.WeatherPlugin.Exceptions;

namespace Chatbot.Plugins.WeatherPlugin
{
    static class ResponseMessage
    {
        public static Message Ok(List<ICommand> commands, WeatherInformation weatherInformation)
        {
            StringBuilder stringBuilder = new StringBuilder($"Wetter in {weatherInformation.CityName} ({weatherInformation.Date}): ");

            commands.ForEach(rs => stringBuilder.AppendLine($"{rs.GetInformation(weatherInformation)}"));

            return new Message(stringBuilder.ToString());
        }

        public static Message CityNotFoundMessage(string city)
        {
            return new Message($"Mein Informant für Wetterdaten hat die Stadt '{city}' nicht gefunden!");
        }

        public static Message Unauthorized()
        {
            return new Message($"Mir wurde der Zugriff auf die Wetterdaten verweigert.");
        }

        public static Message APIError()
        {
            return new Message($"Mein Informant für Wetterdaten ist zurzeit nicht erreichbar. Versuche es später erneut.");
        }

        public static Message UnknownError()
        {
            return new Message($"Mein Informant für Wetterdaten kann die Anfrage nicht beantworten.");
        }
    }
}

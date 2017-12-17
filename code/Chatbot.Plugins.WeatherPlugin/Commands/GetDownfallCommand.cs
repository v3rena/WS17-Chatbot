using Chatbot.Plugins.WeatherPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatbot.Plugins.WeatherPlugin.Models;

namespace Chatbot.Plugins.WeatherPlugin.Commands
{
    class GetDownfallCommand : ICommand
    {
        public string GetInformation(WeatherInformation weatherInformation)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (weatherInformation.Rain != null)
            {
                stringBuilder.Append($"In den letzten 3 Stunden hat es {weatherInformation.Rain.Volume}l geregnet.");
            }
            if (weatherInformation.Snow != null)
            {
                stringBuilder.Append($"In den letzten 3 Stunden hat es {weatherInformation.Snow.Volume}l geschneit.");
            }

            return stringBuilder.ToString();
        }
    }
}

using Chatbot.Plugins.WeatherPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatbot.Plugins.WeatherPlugin.Models;

namespace Chatbot.Plugins.WeatherPlugin.Commands
{
    class GetCloudinessCommand : ICommand
    {
        public string GetInformation(WeatherInformation weatherInformation)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Es ist zu {weatherInformation.Clouds.CloudinessPercentage}% bewölkt."); 
            return stringBuilder.ToString();
        }
    }
}

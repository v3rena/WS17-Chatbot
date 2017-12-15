using Chatbot.Plugins.WeatherPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatbot.Plugins.WeatherPlugin.Models;

namespace Chatbot.Plugins.WeatherPlugin.Commands
{
    class GetTemperatureCommand : ICommand
    {
        private bool showDetails;
        public GetTemperatureCommand(bool showDetails = false)
        {
            this.showDetails = showDetails;
        }
        public string GetInformation(WeatherInformation weatherInformation)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Es hat aktuell {weatherInformation.Main.Temperature}°C");

            if (showDetails)
            {
                if (weatherInformation.Main.MinTemperature != weatherInformation.Main.MaxTemperature)
                {
                    stringBuilder.Append($" (kann zwischen {weatherInformation.Main.MinTemperature}");
                    stringBuilder.Append($" und {weatherInformation.Main.MaxTemperature}°C schwanken)");
                }

            }
            return stringBuilder.Append(".").ToString();
        }
    }
}

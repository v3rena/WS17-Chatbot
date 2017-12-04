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
            stringBuilder.AppendLine($"Temperatur: {weatherInformation.Main.Temperature}°C");

            if (showDetails)
            {
                stringBuilder.AppendLine($"TemperaturMin: {weatherInformation.Main.MinTemperature}°C");
                stringBuilder.AppendLine($"TemperaturMax: {weatherInformation.Main.MaxTemperature}°C");

            }
            return stringBuilder.ToString();
        }
    }
}

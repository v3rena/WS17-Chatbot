using Chatbot.Plugins.WeatherPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatbot.Plugins.WeatherPlugin.Models;

namespace Chatbot.Plugins.WeatherPlugin.Commands
{
    class GetDefaultInformationCommand : ICommand
    {
        public string GetInformation(WeatherInformation weatherInformation)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //TODO check if works
            weatherInformation.Weather.ToList().ForEach(w => stringBuilder.Append($"{w.Description}, "));
            return stringBuilder.Remove(stringBuilder.Length - 2, 2).ToString();
        }
    }
}

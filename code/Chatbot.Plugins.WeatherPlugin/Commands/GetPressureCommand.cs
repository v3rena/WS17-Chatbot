using Chatbot.Plugins.WeatherPlugin.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;

namespace Chatbot.Plugins.WeatherPlugin.Commands
{
    class GetPressureCommand : ICommand
    {
        public string GetInformation(WeatherInformation weatherInformation)
        {
            return $"Luftdruck: {weatherInformation.Main.Pressure}hPa";
        }
    }
}

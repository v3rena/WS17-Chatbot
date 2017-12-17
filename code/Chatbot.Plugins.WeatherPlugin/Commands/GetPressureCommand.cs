using Chatbot.Plugins.WeatherPlugin.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;

namespace Chatbot.Plugins.WeatherPlugin.Commands
{
    class GetPressureCommand : ICommand
    {
        public string GetInformation(WeatherInformation weatherInformation)
        {
            return $"Der Luftdruck beträgt {weatherInformation.Main.Pressure}hPa.";
        }
    }
}

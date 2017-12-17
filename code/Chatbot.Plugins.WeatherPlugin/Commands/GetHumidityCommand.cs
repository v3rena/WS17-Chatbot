using Chatbot.Plugins.WeatherPlugin.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;

namespace Chatbot.Plugins.WeatherPlugin.Commands
{
    class GetHumidityCommand : ICommand
    {
        public string GetInformation(WeatherInformation weatherInformation)
        {
            return $"Die Luftfeuchtigkeit beträgt {weatherInformation.Main.Humidity}%.";
        }
    }
}
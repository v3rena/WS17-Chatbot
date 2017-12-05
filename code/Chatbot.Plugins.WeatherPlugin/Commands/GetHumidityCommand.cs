using Chatbot.Plugins.WeatherPlugin.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;

namespace Chatbot.Plugins.WeatherPlugin.Commands
{
    class GetHumidityCommand : ICommand
    {
        public string GetInformation(WeatherInformation weatherInformation)
        {
            return $"Luftfeuchtigkeit: {weatherInformation.Main.Humidity}%";
        }
    }
}
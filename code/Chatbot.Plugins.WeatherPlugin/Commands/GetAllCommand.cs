using Chatbot.Plugins.WeatherPlugin.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;
using System.Collections.Generic;
using System.Text;

namespace Chatbot.Plugins.WeatherPlugin.Commands
{
    class GetAllCommand : ICommand
    {
        List<ICommand> commands;

        public GetAllCommand()
        {
            commands = new List<ICommand>
            {
                new GetTemperatureCommand(true),
                new GetWeatherDescriptionCommand(),
                new GetHumidityCommand(),
                new GetWindCommand()
            };
        }

        public string GetInformation(WeatherInformation weatherInformation)
        {
            StringBuilder stringBuilder = new StringBuilder();
            commands.ForEach(c => stringBuilder.AppendLine(c.GetInformation(weatherInformation)));
            return stringBuilder.ToString();
        }
    }
}
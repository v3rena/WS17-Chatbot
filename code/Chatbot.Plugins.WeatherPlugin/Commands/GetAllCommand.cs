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
                new GetCloudinessCommand(),
                new GetDownfallCommand(),
                new GetWindCommand(),
                new GetHumidityCommand(),
                new GetPressureCommand()
            };
        }

        public string GetInformation(WeatherInformation weatherInformation)
        {
            StringBuilder stringBuilder = new StringBuilder();
            commands.ForEach(command =>
            {
                string info = command.GetInformation(weatherInformation);
                if (!string.IsNullOrWhiteSpace(info))
                    stringBuilder.AppendLine(info);
            });
            return stringBuilder.ToString();
        }
    }
}
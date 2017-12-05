using System;
using Chatbot.Plugins.WeatherPlugin.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;

namespace Chatbot.Plugins.WeatherPlugin.Commands
{
    class GetWindCommand : ICommand
    {
        static string[] directions = new string[] { "Norden", "Nordnordosten", "Nordosten", "Ostnordosten", "Osten",
            "Ostsüdosten", "Südosten", "Südsüdosten", "Süden", "Südsüdwesten", "Südwesten", "Westsüdwesten", "Westen",
            "Westnordwesten", "Nordwesten", "Nordnordwesten" };
        public string GetInformation(WeatherInformation weatherInformation)
        {
            return $"Der Wind weht mit {CalcSpeed(weatherInformation.Wind.Speed)}km/h von {CalcDirection(weatherInformation.Wind.WindDirectionDegrees)}.";
        }

        private string CalcDirection(int windDirectionDegrees)
        {
            int val = (int)((windDirectionDegrees / 22.5) + 0.5);
            return directions[(val % 16)];
        }

        private float CalcSpeed(float speed)
        {
            return speed / 3.6f;
        }
    }
}
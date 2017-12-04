using System;
using Chatbot.Plugins.WeatherPlugin.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;

namespace Chatbot.Plugins.WeatherPlugin.Commands
{
    class GetWindCommand : ICommand
    {
        static string[] directions = new string[] { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
        public string GetInformation(WeatherInformation weatherInformation)
        {
            return $"Wind: {CalcSpeed(weatherInformation.Wind.Speed)}km/h ({CalcDirection(weatherInformation.Wind.WindDirectionDegrees)})";
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
using Chatbot.Plugins.WeatherPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.WeatherPlugin
{
    class Cache
    {
        private IDictionary<string, WeatherInformation> storedWeatherInformations;

        public Cache()
        {
            storedWeatherInformations = new Dictionary<string, WeatherInformation>();
        }

        public WeatherInformation Get(string city)
        {
            return storedWeatherInformations[city];
        }

        public void Add(string city, WeatherInformation weatherInformation)
        {
            storedWeatherInformations.Add(city, weatherInformation);
        }

        public bool WeatherInformationIsCached(string city)
        {
            if (city != null && storedWeatherInformations.ContainsKey(city))
            {
                return !WeatherInformationIsOutdated(city);
            }
            else
                return false;
        }

        private bool WeatherInformationIsOutdated(string city)
        {
            WeatherInformation weatherInformation = storedWeatherInformations[city];
            bool isOutdated = DateTime.Now.AddMinutes(-10) > weatherInformation.CreationDate;
            if (isOutdated)
            {
                storedWeatherInformations.Remove(city);
            }
            return isOutdated;
        }
    }
}

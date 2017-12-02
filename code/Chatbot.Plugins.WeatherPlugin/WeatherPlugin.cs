using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Chatbot.Plugins.WeatherPlugin
{
    public class WeatherPlugin : IPlugin
    {
        public string Name => "WeatherPlugin";

        #region Private Members

        private static List<string> stringLibrary;
        private IDictionary<string, WeatherInformation> storedWeatherInformations;
        private IDictionary<string, string> defaultConfig;
        private bool CurrentWeatherinformationOfCityIsCached => City != null && storedWeatherInformations.ContainsKey(City) ? !WeatherinformationIsOutDated : false;

        private bool WeatherinformationIsOutDated
        {
            get
            {
                WeatherInformation weatherInformation = storedWeatherInformations[City];
                bool isOutdated = DateTime.Now.AddMinutes(-10) > weatherInformation.CreationDate;
                if (isOutdated)
                {
                    storedWeatherInformations.Remove(City);
                }
                return isOutdated;
            }
        }

        private string City
        {
            get
            {
                return !string.IsNullOrWhiteSpace(city) ? city : defaultCity;
            }
            set
            {
                city = value;
            }
        }

        private string apiKey;
        private string defaultCity;
        private string city;

        private static HttpClient client;

        #endregion

        public WeatherPlugin()
        {
            stringLibrary = new List<string> { "wetter", "temperatur" };
            SetDefaultConfig();
            storedWeatherInformations = new Dictionary<string, WeatherInformation>();
        }

        private void SetDefaultConfig()
        {
            defaultConfig = new Dictionary<string, string>()
            {
                { "ApiKey", "664f03abf48459c28bd6ddfea499f069" },
                { "DefaultCity", "Wien" }
            };
        }

        public float CanHandle(Message message)
        {
            if (stringLibrary.Any(s => message.Content.ToLower().Contains(s)))
                return 1;
            else
                return 0;
        }

        public Message Handle(Message message)
        {
            //TODO replace HelperMethod with textinterpreter
            HelperMethodReadCity(message);

            WeatherInformation result = !CurrentWeatherinformationOfCityIsCached ? CallWeatherApi() : storedWeatherInformations[City];

            //TODO responsive response message
            return new Message($"Die Temperatur in {City} beträgt " + result.Main.Temperature + "°C.");
        }

        private void HelperMethodReadCity(Message message)
        {
            //reset city
            City = null;

            var split = message.Content.Split(null); //splits by whitespace
            for (int i = 0; i < split.Count(); i++)
            {
                if (split[i] == "in" && i + 1 < split.Count())
                {
                    City = split[i + 1];
                    return;
                }
            }
        }

        private WeatherInformation CallWeatherApi()
        {
            client = new HttpClient();
            string url = $"https://api.openweathermap.org/data/2.5/weather?APPID={apiKey}&q={City}&units=metric";
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Get the weatherInformation
                WeatherInformation weatherInformation = GetWeatherInformation(client.BaseAddress);
                storedWeatherInformations.Add(City, weatherInformation);
                return weatherInformation;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                client.Dispose();
            }
        }

        private WeatherInformation GetWeatherInformation(Uri baseAddress)
        {
            HttpResponseMessage response = client.GetAsync(baseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WeatherInformation>(jsonString);
            }
            else
                throw new ApplicationException();
        }

        public IEnumerable<PluginConfiguration> EnsureDefaultConfiguration(IList<PluginConfiguration> configuration)
        {
            defaultConfig.Keys.ToList().ForEach(e =>
            {
                if (!configuration.Keys.ToList().Contains(e))
                    configuration.Add(e, defaultConfig[e]);
            });

            apiKey = configuration["ApiKey"];
            defaultCity = configuration["DefaultCity"];

            return configuration;
        }
    }
}

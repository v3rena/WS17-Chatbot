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

        private string apiKey;
        private string defaultCity;
        private string city;

        private static HttpClient client;

        #endregion

        public string City
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

        public WeatherPlugin()
        {
            //TODO save values in config file 
            apiKey = "664f03abf48459c28bd6ddfea499f069";
            defaultCity = "Wien";
            stringLibrary = new List<string> { "wetter", "temperatur", "regen", "sonne", "wolken" };
            storedWeatherInformations = new Dictionary<string, WeatherInformation>();
            client = new HttpClient();
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
            WeatherInformation result = !CurrentWeatherinformationOfCityIsCached ? CallWeatherApi() : storedWeatherInformations[City];

            return new Message($"Die Temperatur in {City} beträgt " + result.Main.Temperature + "°C.");
        }

        private WeatherInformation CallWeatherApi()
        {
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

        public Dictionary<string, string> EnsureDefaultConfiguration(Dictionary<string, string> configuration)
        {
            //no configuration needed yet
            return configuration;
        }
    }
}

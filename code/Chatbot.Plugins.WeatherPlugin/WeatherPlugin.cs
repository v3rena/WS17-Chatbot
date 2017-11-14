using Chatbot.Interfaces;
using Chatbot.Models;
using Chatbot.Plugins.WeatherPlugin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Chatbot.Plugins.WeatherPlugin
{
    public class WeatherPlugin : IPlugin
    {
        public string Name => "WeatherPlugin";

        private static List<string> stringLibrary = new List<string> { "wetter", "temperatur", "regen", "sonne", "wolken" };
        public string Location { get; set; }
        public string ApiKey { get; }

        static HttpClient client = new HttpClient();

        public WeatherPlugin()
        {
            ApiKey = ConfigurationManager.AppSettings["OpenWeatherMapAPIKey"];
        }

        public float CanHandle(Message message)
        {
            message.Content.ToLower();

            if (stringLibrary.Any(s => message.Content.Contains(s)))
                return 1;
            else
                return 0;   
        }

        public Message Handle(Message message)
        {
            // api.openweathermap.org/data/2.5/weather?q=London

            RunAsync().Wait();

            return null;
        }

        async Task RunAsync()
        {
            client.BaseAddress = new Uri($"api.openweathermap.org/data/2.5/forecast?q=London?id={ApiKey}");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Get the product
                WeatherInformation weatherInformation = await GetWeatherInfromationAsync(client.BaseAddress);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }

        async Task<WeatherInformation> GetWeatherInfromationAsync(Uri baseAddress)
        {
            WeatherInformation weatherInfromation = null;
            HttpResponseMessage response = await client.GetAsync(baseAddress);
            if (response.IsSuccessStatusCode)
            {
                weatherInfromation = await response.Content.ReadAsAsync<WeatherInformation>();
            }
            return weatherInfromation;
        }

        public Dictionary<string, string> EnsureDefaultConfiguration(Dictionary<string, string> configuration)
        {
            //no configuration needed yet
            return configuration;
        }
    }
}

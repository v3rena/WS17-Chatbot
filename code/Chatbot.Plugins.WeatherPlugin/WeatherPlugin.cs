using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Commands;
using Chatbot.Plugins.WeatherPlugin.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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

        private List<ICommand> commands;

        private string apiKey;
        private string defaultCity;
        private string city;
        private string language;

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
            SetDefaultConfig();
            
            stringLibrary = new List<string> { "wetter", "temperatur", "regen", "sonne", "wolken" };
            storedWeatherInformations = new Dictionary<string, WeatherInformation>();
            commands = new List<ICommand>();
            client = new HttpClient();
        }

        private void SetDefaultConfig()
        {
            //TODO save values in config file
            apiKey = "664f03abf48459c28bd6ddfea499f069";
            defaultCity = "Wien";
            language = "de";

            defaultConfig = new Dictionary<string, string>()
            {
                { "ApiKey", apiKey },
                { "DefaultCity", defaultCity },
                { "Language", language }
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
            //TODO use Textinterpreter instead of HelperMethod + use Textinterpreter in SetStates
            HelperMethodReadCity(message);
            SetCommands(message);

            WeatherInformation result = !CurrentWeatherinformationOfCityIsCached ? CallWeatherApi() : storedWeatherInformations[City];

            return CreateResponseMessage(result);
        }

        private Message CreateResponseMessage(WeatherInformation weatherInformation)
        {
            StringBuilder stringBuilder = new StringBuilder($"Wetter in {weatherInformation.CityName}:\n");

            commands.ForEach(rs => stringBuilder.AppendLine($"\t{rs.GetInformation(weatherInformation)}"));

            return new Message(stringBuilder.ToString());
        }

        private void SetCommands(Message message)
        {
            commands.Clear();
            string content = message.Content.ToLower();

            if (content.Contains("detail"))
            {
                commands.Add(new GetAllCommand());
                return;
            }

            if (content.Contains("temperatur") || content.Contains("warm") || content.Contains("kalt") || content.Contains("wie"))
                commands.Add(new GetTemperatureCommand(content.Contains("max") || content.Contains("min")));
            if (content.Contains("wetter") || content.Contains("wie") || content.Contains("schön") || content.Contains("wolke") || content.Contains("regen") ||
                content.Contains("regne") || content.Contains("schnee") || content.Contains("schnei") || content.Contains("nebel"))
                commands.Add(new GetWeatherDescriptionCommand());
            if (content.Contains("feucht") || content.Contains("humid") || content.Contains("nebel"))
                commands.Add(new GetHumidityCommand());
            if (content.Contains("wind") || content.Contains("sturm") || content.Contains("böhe"))
                commands.Add(new GetWindCommand());
        }

        private void HelperMethodReadCity(Message message)
        {
            var split = message.Content.Split(null); //splits by whitespace
            for (int i = 0; i < split.Count(); i++)
            {
                if (split[i] == "in" && i + 1 < split.Count())
                {
                    if (split[i + 1].StartsWith("'"))
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int j = i + 1; j < split.Count(); j++)
                        {
                            stringBuilder.AppendFormat(split[j]);
                            if (split[j].EndsWith("'"))
                            {
                                City = stringBuilder.ToString(1, stringBuilder.Length - 2);
                                return;
                            }
                            stringBuilder.AppendFormat(" ");
                        }
                    }

                    City = split[i + 1];
                    return;
                }
            }
        }

        private WeatherInformation CallWeatherApi()
        {
            client = new HttpClient();
            string url = $"https://api.openweathermap.org/data/2.5/weather?APPID={apiKey}&q={City}&units=metric&lang={language}";
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
            catch (ApplicationException e)
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
            //TODO change IEnumerable to List - otherwise no one can insert configs
            defaultConfig.Keys.ToList().ForEach(e =>
            {
                if (configuration.Where(i => i.Key == e).SingleOrDefault() == null)
                    configuration.Add(new PluginConfiguration() { Name = "WeatherPlugin", Key = e, Value = defaultConfig[e] });
            });

            apiKey = configuration.Where(i => i.Key == "ApiKey").Single().Value;
            defaultCity = configuration.Where(i => i.Key == "DefaultCity").Single().Value;

            return configuration;
        }
    }
}

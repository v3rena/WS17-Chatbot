using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Commands;
using Chatbot.Plugins.WeatherPlugin.Exceptions;
using Chatbot.Plugins.WeatherPlugin.Interfaces;
using Chatbot.Plugins.WeatherPlugin.Models;
using Microsoft.SqlServer.Management.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Xml.Linq;

namespace Chatbot.Plugins.WeatherPlugin
{
    public class WeatherPlugin : IPlugin
    {
        public string Name => "WeatherPlugin";

        #region Private Members

        private IDictionary<string, string> defaultConfig;
        private static List<string> stringLibrary;

        private Cache cache;
        private List<ICommand> commands;
        private string domain;
        private string apiKey;
        private string defaultCity;
        private string city;
        private string language;

        private static HttpClient client;

        private string City
        {
            get
            {
                return !string.IsNullOrWhiteSpace(city) ? city : defaultCity;
            }
            set
            {
                if (value != null)
                    city = value.ToLower();
            }
        }

        #endregion

        public WeatherPlugin()
        {
            ReadDefaultConfiguration();

            domain = "https://api.openweathermap.org";

            //TODO: update library
            stringLibrary = new List<string> { "wetter", "temperatur", "regen", "regne", "schnee", "schnei", "sonne", "wolke",
                "nebel", "kalt", "warm", "wind", "nass" };
            cache = new Cache();
            commands = new List<ICommand>();
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
            //TODO use Textinterpreter instead of HelperMethod + use Textinterpreter in SetCommands
            City = TextInterpreterHelper.HelperMethodReadCity(message);
            SetCommands(message);

            try
            {
                WeatherInformation result = cache.WeatherInformationIsCached(City) ? cache.Get(City) : CallWeatherApi();
                return ResponseMessage.Ok(commands, result);
            }
            catch (CityNotFoundException)
            {
                return ResponseMessage.CityNotFoundMessage(City);
            }
            catch (APIUnauthorizedException)
            {
                return ResponseMessage.Unauthorized();
            }
            catch (APIErrorException)
            {
                return ResponseMessage.APIError();
            }
            catch (UnknownErrorException)
            {
                return ResponseMessage.UnknownError();
            }
        }

        private void SetCommands(Message message)
        {
            commands.Clear();
            commands.Add(new GetDefaultInformationCommand());

            string content = message.Content.ToLower();

            if (content.Contains("detail"))
            {
                commands.Add(new GetAllCommand());
                return;
            }

            if (content.Contains("temperatur") || content.Contains("warm") || content.Contains("kalt") || content.Contains("wie"))
                commands.Add(new GetTemperatureCommand(content.Contains("max") || content.Contains("min")));
            if (content.Contains("wetter") || content.Contains("wie") || content.Contains("schön") || content.Contains("wolke") ||
                content.Contains("bewölk") || content.Contains("nebel"))
                commands.Add(new GetCloudinessCommand());
            if (content.Contains("regen") || content.Contains("regne") || content.Contains("schnee") || content.Contains("schnei") ||
                content.Contains("niederschlag") || content.Contains("niesel") || content.Contains("nass"))
                commands.Add(new GetDownfallCommand());
            if (content.Contains("wind") || content.Contains("sturm") || content.Contains("böhe") || content.Contains("weht") || 
                content.Contains("wehen"))
                commands.Add(new GetWindCommand());
            if (content.Contains("feucht") || content.Contains("humid") || content.Contains("nebel"))
                commands.Add(new GetHumidityCommand());
            if (content.Contains("druck"))
                commands.Add(new GetPressureCommand());
        }

        private WeatherInformation CallWeatherApi()
        {
            client = new HttpClient();
            string url = $"{domain}/data/2.5/weather?APPID={apiKey}&q={City}&units=metric&lang={language}";
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Get the weatherInformation
                WeatherInformation weatherInformation = GetWeatherInformation(client.BaseAddress);
                cache.Add(City, weatherInformation);
                return weatherInformation;
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
            {
                var errorResponse = GetErrorResponse(response.Content);
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new CityNotFoundException(errorResponse.Item1, errorResponse.Item2);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new APIUnauthorizedException(errorResponse.Item1, errorResponse.Item2);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new APIErrorException(errorResponse.Item1, errorResponse.Item2);
                }
                else
                {
                    throw new UnknownErrorException(errorResponse.Item1, errorResponse.Item2);
                }
            }
        }

        private Tuple<int, string> GetErrorResponse(HttpContent content)
        {
            var json = content.ReadAsStringAsync().Result;
            var JSONObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return new Tuple<int, string>(int.Parse(JSONObj["cod"]), JSONObj["message"]);
        }

        #region Configuration
        private void ReadDefaultConfiguration()
        {
            defaultConfig = new Dictionary<string, string>();

            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configFileName = Path.Combine(assemblyFolder, "Chatbot.Plugins.WeatherPlugin.dll.config");

            XDocument config = XDocument.Load(configFileName);
            XElement root = config.Element("configuration");
            XElement appsettings = root.Element("appSettings");

            appsettings.Elements().ToList().ForEach(e => defaultConfig.Add(e.Attribute("key").Value, e.Attribute("value").Value));
        }

        public IDictionary<string, string> EnsureDefaultConfiguration(IDictionary<string, string> configuration)
        {
            AddMissingValuesToConfiguration(configuration);
            SetConfiguration(configuration);

            return configuration;
        }

        public void RefreshConfiguration(IDictionary<string, string> configuration)
        {
            SetConfiguration(configuration);
        }

        private void AddMissingValuesToConfiguration(IDictionary<string, string> configuration)
        {
            defaultConfig.AsParallel().ForAll(element =>
            {
                if (!configuration.ContainsKey(element.Key))
                    configuration.Add(element.Key, element.Value);
                else if (string.IsNullOrWhiteSpace(configuration[element.Key]))
                    configuration[element.Key] = element.Value;
            });
        }

        private void SetConfiguration(IDictionary<string, string> configuration)
        {
            try
            {
                apiKey = configuration["ApiKey"];
                defaultCity = configuration["DefaultCity"];
                language = defaultConfig["Language"];
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidArgumentException("Key not found.", e);
            }
        }

        #endregion
    }
}

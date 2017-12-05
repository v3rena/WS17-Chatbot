using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Chatbot.Plugins.WeatherPlugin.Models
{
    class WeatherInformation
    {
        public WeatherInformation()
        {
            CreationDate = DateTime.Now;
        }

        [JsonProperty("id")]
        public int CityId { get; set; }

        [JsonProperty("name")]
        public string CityName { get; set; }

        [JsonProperty("weather")]
        public IList<WeatherInformationWeather> Weather { get; set; }

        [JsonProperty("main")]
        public WeatherInformationMain Main { get; set; }

        [JsonProperty("visibilty")]
        public long Visibilty { get; set; }

        [JsonProperty("wind")]
        public WeatherInformationWind Wind { get; set; }

        [JsonProperty("clouds")]
        public WeatherInformationClouds Clouds { get; set; }

        [JsonProperty("rain")]
        public WeatherInformationDownfall Rain { get; set; }

        [JsonProperty("snow")]
        public WeatherInformationDownfall Snow { get; set; }

        [JsonProperty("dt")]
        private double? DateUnixTimestamp { get; set; }
        public DateTime? Date => DateUnixTimestamp != null ? new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double)DateUnixTimestamp) : (DateTime?)null;

        public DateTime CreationDate { get; }
    }

    #region Subclasses

    public class WeatherInformationWeather
    {
        [JsonProperty("id")]
        public int WeatherConditionId { get; set; }

        [JsonProperty("main")]
        public string WeatherParameter { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class WeatherInformationMain
    {
        [JsonProperty("temp")]
        public string Temperature { get; set; }

        [JsonProperty("pressure")]
        public string Pressure { get; set; }

        [JsonProperty("humidity")]
        public string Humidity { get; set; }

        [JsonProperty("temp_min")]
        public string MinTemperature { get; set; }

        [JsonProperty("temp_max")]
        public string MaxTemperature { get; set; }
    }


    public class WeatherInformationWind
    {
        [JsonProperty("speed")]
        public float Speed { get; set; }

        [JsonProperty("deg")]
        public int WindDirectionDegrees { get; set; }
    }
    public class WeatherInformationClouds
    {
        [JsonProperty("all")]
        public int CloudinessPercentage { get; set; }
    }

    public class WeatherInformationDownfall
    {
        [JsonProperty("3h")]
        public int Volumne { get; set; }
    }

    #endregion

}
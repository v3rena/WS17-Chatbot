using System;
using Newtonsoft.Json;

namespace Chatbot.Plugins.RoutePlannerBot
{
    class DirectionsResponseConverter
    {
        static private string responseTemplate = "Dauer: {0}<br>{1}";

        public static string GetHumanReadableDirections(string json)
        {
            string directions = "";
            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            try
            {
                directions = applyTemplate(jsonObject);
            }
            catch (Exception)
            {
                directions = "Ich konnte leider nichts finden :/"; // todo nicht hardcoden
            }
            return directions;
        }

        private static string applyTemplate(dynamic jsonObject)
        {
            dynamic leg = jsonObject.routes[0].legs[0];
            return string.Format(responseTemplate,
                leg.duration.text,
                getAllSteps(leg.steps));
        }

        private static string getAllSteps(dynamic steps)
        {
            string htmlSteps = "";
            foreach (dynamic step in steps)
            {
                htmlSteps += step.html_instructions + "<br>";
            }
            return htmlSteps;
        }
    }
}

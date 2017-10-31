using System.IO;
using System.Net;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;

namespace Chatbot.Plugins.RoutePlannerBot
{
    class DirectionsRequest
    {
        private string apiKey;
        private string origin, destination;

        public DirectionsRequest() {
            apiKey = ConfigurationManager.AppSettings.Get("googleApiKey");
        }

        public DirectionsRequest(string origin, string destination)
        {
            this.origin = HttpUtility.UrlEncode(origin);
            this.destination = HttpUtility.UrlDecode(destination);
        }

        public string GetResponse()
        {
            string url = string.Format("https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&mode=transit&key={2}", origin, destination, apiKey);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Accept-Language:de");
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            return reader.ReadToEnd();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chatbot.Plugins.RoutePlannerBot
{
    class DirectionsRequest
    {
        private string origin, destination, apiKey;

        public DirectionsRequest() { }

        public DirectionsRequest(string origin, string destination, string apiKey)
        {
            this.origin = HttpUtility.UrlEncode(origin);
            this.destination = HttpUtility.UrlDecode(destination);
            this.apiKey = apiKey;
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

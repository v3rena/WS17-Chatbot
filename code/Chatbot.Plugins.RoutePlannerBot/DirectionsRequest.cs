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
        // TODO read API key from config file
        private string apiKey = "AIzaSyA-lYBx2cblAh7I4tID_Db2lornpVyjNWU";
        private string origin, destination;

        public DirectionsRequest() { }

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

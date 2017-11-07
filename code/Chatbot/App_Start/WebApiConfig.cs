using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Chatbot
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web-API-Konfiguration und -Dienste

            // Web-API-Routen
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "MessageApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { action = "PostMessage", id = RouteParameter.Optional },
            //    constraints: new { id = @"^[0-9]*$" }
            //);
        }
    }
}

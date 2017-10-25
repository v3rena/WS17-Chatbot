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

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { action = "Default", id = RouteParameter.Optional },
                constraints: new { id = @"^[0-9]*$" }
            );
        }
    }
}

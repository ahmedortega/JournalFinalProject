using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JournalProjectWebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            config.Routes.MapHttpRoute(
                name: "DefaultRpc",
                routeTemplate: "rpc/{controller}/{action}/{usertype}/{id}",
                defaults: new { id = RouteParameter.Optional,
                action = RouteParameter.Optional,
                usertype = RouteParameter.Optional}
                );
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        }
    }
}

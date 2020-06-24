using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using SignalRHubWebApi.Hubs;
using System.Net.Http.Headers;
using log4net;
using log4net.Config;

namespace SignalRHubWebApi
{
    public static class WebApiConfig
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(WebApiConfig));
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            try
            {
                MyHub myhub = new MyHub();
                BasicConfigurator.Configure();
                logger.Info("SignalRHub Started "+ DateTime.Now);
                config.SuppressDefaultHostAuthentication();
                config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

                // Web API routes
                config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
                Global.LogMessage = myhub.SendToUniqueClient;
                config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            }
            catch(Exception ex)
            {
                logger.Error("-----------------------------------------------");
                logger.Error("Problem in Register method. Error: " + ex.Message);
                logger.Error("-----------------------------------------------");
            }
        }
    }
}

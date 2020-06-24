using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using log4net;
using log4net.Config;

[assembly: OwinStartup(typeof(SignalRHubWebApi.Startup))]

namespace SignalRHubWebApi
{
    public partial class Startup
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Startup));
        public void Configuration(IAppBuilder app)
        {
            try
            {
                BasicConfigurator.Configure();
                app.UseCors(CorsOptions.AllowAll);
                app.MapSignalR();
            }
            catch(Exception ex)
            {
                logger.Error("-----------------------------------------------");
                logger.Error("Problem in Startup Configuration method. Error: " + ex.Message);
                logger.Error("-----------------------------------------------");
            }

        }
    }
}

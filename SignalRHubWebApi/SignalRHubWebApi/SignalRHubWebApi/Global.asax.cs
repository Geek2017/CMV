using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SignalRHubWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(WebApiApplication));
        protected void Application_Start()
        {
            try
            {
                BasicConfigurator.Configure();
                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
            }
            catch(Exception ex)
            {
                logger.Error("-----------------------------------------------");
                logger.Error("Problem in application start. Error: " + ex.Message);
                logger.Error("-----------------------------------------------");
            }
        }
        protected void Application_End(object sender, EventArgs e)
        {
            //Need: To track HUB Shutdown
            BasicConfigurator.Configure();
            logger.Info("SignalRHub- application closed at: " + DateTime.Now);

        }
    }
}

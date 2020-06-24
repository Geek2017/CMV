using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalRHubWebApi.Hubs;
using Microsoft.AspNet.SignalR;
using SignalRHubWebApi.Controllers;

namespace SignalRHubWebApi
{
    public partial class CallMonitor : System.Web.UI.Page
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CallMonitor));
        string custId = string.Empty;
        string clientPhoneNumber = string.Empty;
        string clientExtension = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAllParamsFound = true;

            if (Request.QueryString["custid"] != null)
            {
                custId = Request.QueryString["custid"].ToString();
            }
            else
            {
                isAllParamsFound = false;
                logger.Info("Customer Id as custid is empty");
                Response.Write("Customer Id is empty");
            }

            if (Request.QueryString["calleridnumber"] != null)
            {
                clientPhoneNumber = Request.QueryString["calleridnumber"].ToString();
            }
            else
            {
                isAllParamsFound = false;
                logger.Info("calleridnumber as calleridnumber is empty");
                Response.Write("Caller number is empty");

            }

            if (Request.QueryString["clientExtension"] != null)
            {
                clientExtension = Request.QueryString["clientExtension"].ToString();
            }
            else
            {
                isAllParamsFound = false;
                logger.Info("ClientExtension as clientExtension is empty");
                Response.Write("Agent extension is empty");
            }

            //Call the API if all three parameters found
            if (isAllParamsFound)
            {
                CallAPI();
            }
        }

        private void CallAPI()
        {
            try
            {
                ValuesController mh = new ValuesController();
                mh.Get(clientExtension, custId, clientPhoneNumber);
            }
            catch (Exception ex)
            {
                logger.Error("-----------------------------------------------");
                logger.Error("Problem in CallMonitor calling API. Error: " + ex.Message);
                logger.Error("-----------------------------------------------");
            }
        }
    }
}
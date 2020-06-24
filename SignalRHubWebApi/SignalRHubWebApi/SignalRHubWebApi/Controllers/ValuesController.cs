using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SignalRHubWebApi.Controllers
{
   
    public class ValuesController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ValuesController));
       
        // GET api/values/5/9999999991
        [Route("api/values/{extNo}/{custId}/{phoneNo}")]
        [HttpGet]
        public string Get(string extNo, string custId, string phoneNo)
        {
            try
            {
                BasicConfigurator.Configure();
                string salesforceUrl = string.Empty;
                //Send to unique client method called.
                logger.Info("API Hit Extension : " + extNo + ", CustId : " + custId + ", PhoneNo:" + phoneNo);
                Global.LogMessage("", salesforceUrl, custId, extNo,phoneNo);
                return "Agent Extension : "+ extNo + ", CustId : "+ custId +", PhoneNo:"+phoneNo;
            }
            catch (Exception ex)
            {
                logger.Error("-------------------------------------------------\n");
                logger.Error("Problem in ValuesController to Get API method. Error: " + ex.Message);
                logger.Error("-------------------------------------------------\n");
                return "";
            }
        }
    }
}

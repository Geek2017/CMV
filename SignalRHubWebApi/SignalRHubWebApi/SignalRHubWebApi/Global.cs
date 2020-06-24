using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRHubWebApi
{
    public class Global
    {
        public delegate void DelLogMessage(string connectionId, string salesforceUrl,string customerId, string extensionNo, string phoneNo);
        public static DelLogMessage LogMessage;
    }
}
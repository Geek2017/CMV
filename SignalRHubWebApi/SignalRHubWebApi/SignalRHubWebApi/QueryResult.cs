using SignalRHubWebApi.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRHubWebApi
{
    public class QueryResult
    {
        public List<SearchRecord> searchRecords { get; set; }
    }
}
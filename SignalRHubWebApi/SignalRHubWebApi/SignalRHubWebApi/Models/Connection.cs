using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SignalRHubWebApi.Models
{
    public class Connection
    {
        [Key]
        public int Id { get; set; }
        public string AgentConnectionId { get; set; }
        public string CustomerId { get; set; }
        public string ExtensionNo { get; set; }
        public Boolean IsConnected { get; set; }
        public DateTime ConnectionTime { get; set; }
    }

}
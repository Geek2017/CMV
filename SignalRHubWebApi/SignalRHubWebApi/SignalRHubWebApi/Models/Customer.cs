using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SignalRHubWebApi.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string InstanceName { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecretKey { get; set; }
        public string CallbackURL { get; set; }
        public string APIEndPoint { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }
        public string CRMUsed { get; set; }
        public Boolean IsActive { get; set; }
    }
}
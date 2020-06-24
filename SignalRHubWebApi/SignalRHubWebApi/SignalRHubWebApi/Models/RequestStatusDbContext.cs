
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SignalRHubWebApi.Models
{
    public class RequestStatusDbContext : DbContext
    {
        /*Constructor to pass the connection string name to the 
          DbContext base class*/
        public RequestStatusDbContext() : base("name=DBContext")
        { }

        // The RequestStatus db context.
        public DbSet<RequestStatus> ReqStatus { get; set; }

    }
}
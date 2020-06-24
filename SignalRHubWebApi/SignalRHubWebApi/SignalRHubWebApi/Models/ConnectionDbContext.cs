using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SignalRHubWebApi.Models
{
    public class ConnectionDbContext : DbContext
    {
        /*Constructor to pass the connection string name to the 
          DbContext base class*/
        public ConnectionDbContext() : base("name=DBContext")
        { }

        // The Connection db context.
        public DbSet<Connection> Connections { get; set; }

    }
}
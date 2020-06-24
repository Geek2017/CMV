using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SignalRHubWebApi.Models
{
    public class CustomerDbContext : DbContext
    {
        /*Constructor to pass the connection string name to the 
          DbContext base class*/
        public CustomerDbContext() : base("name=DBContext")
        { }

        // The Customer db context.
        public DbSet<Customer> Connections { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CustomerDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

    }
}
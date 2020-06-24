using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MVCAgent.Model
{
    public class RequestStatusDbContext : DbContext
    {
        /*Constructor to pass the connection string name to the 
          DbContext base class*/
        public RequestStatusDbContext() : base("name=DBContext")
        { }

        // The Connection db context.
        public DbSet<RequestStatus> ReqStatus { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAgent.Model
{
    public class RequestStatus
    {
        [Key]
        public int Id { get; set; }
        public string RequestFrom { get; set; }
        public string RequestTo { get; set; }
        public string AgentConnectionId { get; set; }
        public Boolean IsSuccess { get; set; }
        public DateTime Datetime { get; set; }
    }
}

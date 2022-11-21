using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_API.Logic
{
    public class TicketRecord
    {
        public Ticket? T { get; set; }
        public int? EmplID { get; set; }

        public TicketRecord(Ticket? t, int? emplID)
        {
            T = t;
            EmplID = emplID;
        }
    }
}

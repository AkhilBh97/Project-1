using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Logic
{
    public class Ticket
    {
        //Fields
        //public int TicketID { get; set; }
        public string EmplEmail { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        


        //Constructors
        public Ticket() { }

        public Ticket(string email, string desc, double amount, string status = "Pending")
        {
            this.EmplEmail = email;
            this.Description = desc;
            this.Amount = amount;
            this.Status = status;
        }

        //Methods


    }
}

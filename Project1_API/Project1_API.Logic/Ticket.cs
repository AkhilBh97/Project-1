using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_API.Logic
{
    public class Ticket
    {
        //Fields
        //private static int numtickets; //

        public int? TicketID { get; set; }
        public string? EmplEmail { get; set; }
        public string? Description { get; set; }
        public double? Amount { get; set; }
        public string? Status { get; set; }

        //Constructors
        public Ticket() { }

        public Ticket(int id, string email, string desc, double amount, string status = "Pending")
        {
            this.TicketID = id;
            this.EmplEmail = email;
            this.Description = desc;
            this.Amount = amount;
            this.Status = status;
        }

        //Methods
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Ticket Number: {TicketID}\tEmployee Email: {EmplEmail}");
            sb.AppendLine($"Amount Requested: ${Amount}");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine($"Status: {Status}");

            return sb.ToString();
        }

    }
}

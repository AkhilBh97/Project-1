using Project1.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project1.Data
{
    public interface IRepository
    {
        //I want to be able to hash strings here (namely passwords)
        public string Hash(string s);

        //I want to get all the tickets matching either an email or a status
        //Either get a Queue of pending tickets, or a List of all an employee's tickets 
        IEnumerable<Ticket> getAllTickets(bool code, string search);

        Ticket CreateTicket(string email, double amount, string desc);

        //I want to store User info here
        Employee CreateEmployee(string email, string password, string? role);
        Employee GetEmployee();
    }
}

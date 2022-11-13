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
        //I want to get all the tickets matching either an email or a status
        IEnumerable<Ticket> getAllTickets(string? email, string? status);
        Ticket CreateNewTicket(string email, double amount, string desc);

        //I want to store User info here
        Employee CreateNewEmployee(string email, string password, string? role);
        Employee getEmployee();
    }
}

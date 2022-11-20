
using Project1_API.Logic;
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
        Queue<Ticket> GetTicketQueue(string status);
        List<Ticket> GetTicketList(int emplId, string email, string? status);

        Ticket CreateTicket(int emplId, double amount, string desc);
        void UpdateTicketStatus(int id, string status);

        //I want to store User info here
        Employee CreateEmployee(string email, string password, string? role);
        //Check that the password matches what's in the DB
        Employee? GetEmployee(string email, string password);
    }
}

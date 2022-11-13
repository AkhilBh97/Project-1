using Project1.Logic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Data
{
    public class SqlRepository : IRepository
    {
        //Fields
        private readonly string _connectionstring;

        //Constructors
        public SqlRepository(string connectionstring)
        {
            this._connectionstring = connectionstring ?? throw new ArgumentNullException(nameof(connectionstring));
        }

        //Methods
        public Ticket CreateNewTicket(string email, double amount, string desc)
        {
            using SqlConnection connection = new SqlConnection(this._connectionstring);
            //create a new ticket given an employee email, amount, and description
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> getAllTickets(string? email, string? status)
        {
            using SqlConnection connection = new SqlConnection(this._connectionstring);
            //if email is null, then query P1.Tickets for tickets matching a status
            //if status is null, then query P1.Tickets for a certain employee's tickets
            throw new NotImplementedException();
        }

        public Employee CreateNewEmployee(string email, string password, string? role)
        {
            using SqlConnection connection = new SqlConnection(this._connectionstring);
            //connection.Open();
            throw new NotImplementedException();
        }

        public Employee getEmployee()
        {
            throw new NotImplementedException();
        }
    }
}

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
        public IEnumerable<Ticket> getAllTickets(string? email, string? status)
        {
            using SqlConnection connection = new SqlConnection(this._connectionstring);
            //if email is null, then query P1.Tickets for tickets matching a status
            //if status is null, then query P1.Tickets for a certain employee's tickets
            throw new NotImplementedException();
        }

        public Ticket CreateNewTicket(string email, double amount, string desc)
        {
            using SqlConnection connection = new SqlConnection(this._connectionstring);
            connection.Open();
            //create a new ticket given an employee email, amount, and description
            string cmdS = @"INSERT INTO P1.Ticket (EmplEmail, Amount, Description) VALUES (@email, @amt, @desc);";

            using SqlCommand cmd1 = new SqlCommand(cmdS, connection);
            cmd1.Parameters.AddWithValue("@email", email);
            cmd1.Parameters.AddWithValue("@amt", amount);
            cmd1.Parameters.AddWithValue("@desc", desc);

            cmd1.ExecuteNonQuery();

            //now to get the ticket back from the database
            //the status is presumed to be Pending, so we will search for that
            cmdS = @"SELECT TicketID, EmplEmail, Description, Amount, Status FROM P1.Ticket WHERE EmplEmail=@email AND Status=@status;";

            using SqlCommand cmd2 = new SqlCommand(cmdS, connection);
            cmd2.Parameters.AddWithValue("@email", email);
            cmd2.Parameters.AddWithValue("@status", "Pending");

            using SqlDataReader reader = cmd2.ExecuteReader();

            Ticket tmpTicket;
            while (reader.Read())
            {
                return tmpTicket = new Ticket(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDouble(3), reader.GetString(4) );
            }

            connection.Close();
            //Insertion failed
            Ticket noticket = new();
            return noticket;
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

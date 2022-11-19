using Project1.Logic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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

        //Hash a string and return as a byte array
        public string Hash(string s)
        {
            //convert the string to a byte array first
            byte[] hash, inbyte = Encoding.ASCII.GetBytes(s);

            using (SHA1 sha1 = SHA1.Create())
            {
                hash = sha1.ComputeHash(inbyte);
            }
            return "0x" + BitConverter.ToString(hash).Replace("-", "");
        }

        //Get all tickets from the DB
        public IEnumerable<Ticket> getAllTickets(bool code, string search)
        {
            IEnumerable<Ticket> result;
            //the condition is different
            StringBuilder cmdSB = new();
            cmdSB.Append("SELECT * FROM P1.Ticket WHERE ");
            using SqlConnection connection = new SqlConnection(this._connectionstring);
            connection.Open();
            //if code is true: Query for all tickets where status is pending, return a Queue
            //if code is false: Query for all tickets from an email, return a List
            result = code ? new Queue<Ticket>() : new List<Ticket>();
            
            //now append the right condition to the query string


            return result;
        }

        //Insert a new Ticket into the DB
        public Ticket CreateTicket(string email, double amount, string desc)
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

        //Insert an Employee in the DB
        public Employee CreateEmployee(string email, string password, string? role)
        {
            //hash the password here
            string passwordHash = Hash(password);
            
            //just make sure the role is either Employee or Manager
            //if null is passed, default to Employee
            role ??= "Employee";

            using SqlConnection connection = new SqlConnection(this._connectionstring);
            connection.Open();

            //SHA1 hashes a byte array to an array of 40 bytes
            //converting that array to a string, removing dashes, and prepending 0x means all hashes are 42 bytes
            string cmdS = @"INSERT INTO P1.Employee (Email, Password, Role) VALUES (@email, CONVERT(varbinary(42), @pass, 1), @role);";

            using SqlCommand cmd1 = new SqlCommand(cmdS, connection);
            cmd1.Parameters.AddWithValue("@email", email);
            cmd1.Parameters.AddWithValue("@pass", passwordHash);
            cmd1.Parameters.AddWithValue("@role", role);

            cmd1.ExecuteNonQuery();

            //now get that Employee back, but just the email and role
            cmdS = @"SELECT Email, Role FROM P1.Employee WHERE Email=@email;";

            using SqlCommand cmd2 = new SqlCommand(cmdS, connection);
            cmd2.Parameters.AddWithValue("@email", email);

            using SqlDataReader reader = cmd2.ExecuteReader();
            Employee tmpEmp;
            while (reader.Read())
            {
                return tmpEmp = new Employee(reader.GetString(0), reader.GetString(1));
            }
            connection.Close();

            Employee e = new();
            return e;
        }

        public Employee GetEmployee()
        {
            throw new NotImplementedException();
        }
    }
}

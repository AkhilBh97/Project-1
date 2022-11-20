﻿
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Project1_API.Logic;

namespace Project1.Data
{
    public class SqlRepository : IRepository
    {
        //Fields
        private string? _connectionstring;

        //Constructors
        public SqlRepository(){}

        //Methods

        //Set the connection string here
        public void setConnectionString(string? connectionstring)
        {
            this._connectionstring = connectionstring ?? throw new ArgumentNullException(nameof(connectionstring));
        }

        //Hash a string and return as a byte array
        public string Hash(string s)
        {
            //convert the string to a byte array first
            byte[] hash, inbyte = Encoding.ASCII.GetBytes(s);

            using (SHA1 sha1 = SHA1.Create())
            {
                //ComputeHash takes a byte array and returns 40 bytes, separated by dashes
                hash = sha1.ComputeHash(inbyte);
            }
            //remove the dashes and prepend 0x to mark the hash as a hexadecimal string
            return "0x" + BitConverter.ToString(hash).Replace("-", "");
        }

        //Return a queue of tickets
        //Most likely this will just get a queue of pending tickets, but we're passing in a status
        public Queue<Ticket> GetTicketQueue(string status)
        {
            Queue<Ticket> tickets = new Queue<Ticket>();
            using SqlConnection connection = new SqlConnection(this._connectionstring);
            connection.Open();

            string cmdS = $"SELECT TicketID, Email, Description, Amount, Status FROM P1.Ticket, P1.Employee WHERE P1.Employee.ID=P1.Ticket.EmplID AND Status='{status}';";

            using SqlCommand cmd = new(cmdS, connection);

            using SqlDataReader reader = cmd.ExecuteReader();

            Ticket tmpTicket;
            while (reader.Read())
            {
                Console.WriteLine("Reading the pending tickets here...");
                tmpTicket = new Ticket(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    Decimal.ToDouble(reader.GetDecimal(3)), reader.GetString(4));
                tickets.Enqueue(tmpTicket);

                Console.WriteLine($"Here's the Ticket: {tmpTicket.ToString()}");
            }
            connection.Close();
            return tickets;
        }

        //An employee is calling this method, so they will pass their ID and email
        public List<Ticket> GetTicketList(int emplID, string email)
        {
            List<Ticket> tickets = new List<Ticket>();
            using SqlConnection connection = new SqlConnection(this._connectionstring);
            connection.Open();

            string cmdS = @"SELECT * FROM P1.Ticket WHERE EmplID=@emplid;";

            using SqlCommand cmd = new(cmdS, connection);
            cmd.Parameters.AddWithValue("@emplid", emplID);

            using SqlDataReader reader = cmd.ExecuteReader();

            //      TicketID    EmplID  Description     Amount  Status
            Ticket tmpTicket;
            while (reader.Read())
            {
                //Amount column stored as a Decimal in the table, so convert it to a double before passing it 
                tmpTicket = new Ticket(reader.GetInt32(0), email, reader.GetString(2),
                    Decimal.ToDouble(reader.GetDecimal(3)), reader.GetString(4));
                tickets.Add(tmpTicket);
            }
            connection.Close();
            return tickets;
        }

        //Insert a new Ticket into the DB
        public Ticket CreateTicket(int emplId, double amount, string desc)
        {
            using SqlConnection connection = new SqlConnection(this._connectionstring);
            connection.Open();
            //create a new ticket given an employee email, amount, and description
            string cmdS = @"INSERT INTO P1.Ticket (EmplID, Amount, Description) VALUES (@emplid, @amt, @desc);";

            using SqlCommand cmd1 = new SqlCommand(cmdS, connection);
            cmd1.Parameters.AddWithValue("@emplid", emplId);
            cmd1.Parameters.AddWithValue("@amt", amount);
            cmd1.Parameters.AddWithValue("@desc", desc);

            cmd1.ExecuteNonQuery();

            //now to get the ticket back from the database
            //the status is presumed to be Pending, so we will search for that
            cmdS = "SELECT TicketID, Email, Description, Amount, Status FROM P1.Ticket, P1.Employee WHERE P1.Employee.ID=P1.Ticket.EmplID AND Status='Pending';";

            //      TicketID    EmplID  Description     Amount  Status
            using SqlCommand cmd2 = new SqlCommand(cmdS, connection);

            using SqlDataReader reader = cmd2.ExecuteReader();

            Ticket tmpTicket;
            while (reader.Read())
            {
                //[1] is the EmplID, which we do not need here
                //EmplID just links Ticket table to the Employee table
                return tmpTicket = new Ticket(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), 
                    Decimal.ToDouble(reader.GetDecimal(3)), reader.GetString(4) );
            }

            connection.Close();
            //Insertion failed
            Ticket noticket = new();
            return noticket;
        }

        //Update a ticket's status in the DB
        //id: the TicketID 
        //status: The new status, either approved or denied
        public void UpdateTicketStatus(int id, string status)
        {
            using SqlConnection connection = new(this._connectionstring);
            connection.Open();

            string cmdS = $"UPDATE P1.Ticket SET Status='{status}' WHERE TicketID={id};";
            using SqlCommand cmd1 = new SqlCommand(cmdS, connection);
            //cmd1.Parameters.AddWithValue("@status", status);
            //cmd1.Parameters.AddWithValue("@id", id);

            cmd1.ExecuteNonQuery();
            cmd1.Dispose();
            connection.Close();
            return;
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
            //converting that array to a string, removing dashes, and prepending 0x means all hashes are strings of len 42
            //converting that string to a varbinary gave us a 20-byte binary string, so we convert to binary(20)
            string cmdS = @"INSERT INTO P1.Employee (Email, Password, Role) VALUES (@email, CONVERT(binary(20), @pass, 1), @role);";

            using SqlCommand cmd1 = new SqlCommand(cmdS, connection);
            cmd1.Parameters.AddWithValue("@email", email);
            cmd1.Parameters.AddWithValue("@pass", passwordHash);
            cmd1.Parameters.AddWithValue("@role", role);

            cmd1.ExecuteNonQuery();

            //now get that Employee back, but just the email and role
            cmdS = @"SELECT ID, Email, Role FROM P1.Employee WHERE Email=@email;";

            using SqlCommand cmd2 = new SqlCommand(cmdS, connection);
            cmd2.Parameters.AddWithValue("@email", email);

            using SqlDataReader reader = cmd2.ExecuteReader();
            Employee tmpEmp;
            while (reader.Read())
            {
                return tmpEmp = new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            }
            connection.Close();

            Employee e = new();
            return e;
        }

        public Employee? GetEmployee(string? email, string? password)
        {   
            //either password or email are null, return a null object
            if (password == null || email==null) return null;
            //hash the password and use that to search for the Employee
            string passwordHash = Hash(password);

            using SqlConnection connection = new(this._connectionstring);
            connection.Open();

            //Because the password is hashed, 
            string cmdS = $"SELECT ID, Email, Role FROM P1.Employee WHERE Email='{email}' AND Password=CONVERT(binary(20), '{passwordHash}', 1);";

            using SqlCommand cmd1 = new SqlCommand(cmdS, connection);

            using SqlDataReader reader = cmd1.ExecuteReader();
            
            while (reader.Read())
            {
                Employee employee = new();
                employee.Id = reader.GetInt32(0);
                employee.Email = reader.GetString(1);
                employee.Role = reader.GetString(2);

                return employee;
            }
            connection.Close();

            //if nothing returned by the reader, return a null object
            
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Logic
{
    public class Employee
    {
        //Fields
        public string Email { get; set; }
        //readonly because this should not be changed
        private readonly string _password;
        public string Role { get; set; }

        //Constructors
        public Employee(string email, string password, string role = "Employee")
        {
            this.Email = email;
            this._password = password;
            this.Role = role;
            
        }

        //Methods
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Email: {Email}\tRole: {Role}");
            //sb.AppendLine($"Role: {Role}");
            return sb.ToString();
        }
    }
}


using System.Text;

namespace Project1_Client.Logic
{
    public class Employee
    {
        //Fields
        public int? Id { get; set; }
        public string? Email { get; set; }
        //readonly because this should not be changed
        public string? Role { get; set; }

        //Constructors
        public Employee() { }
        public Employee(int? id, string? email, string? role = "Employee")
        {
            this.Id = id;
            this.Email = email;
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


using Project1.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project1.Test
{
    public class EmployeeTests
    {
        [Theory]
        [InlineData("bob@server.net", "Employee")]
        [InlineData("bill@server.net", "Manager")]
        [InlineData("tom@server2.net", "Manager")]
        public void Test1(string email, string role)
        {
            
            Employee test = new Employee(email, role);
            var expected = $"Email: {email}\tRole: {role}\r\n";

            Assert.Equal(expected, test.ToString());
        }

    }
}

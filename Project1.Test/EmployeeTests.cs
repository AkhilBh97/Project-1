
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
        [InlineData("bob@server.net", "password123", "Employee")]
        [InlineData("bill@server.net", "password456", "Manager")]
        [InlineData("tom@server.net", "password789", null)]
        public void EmployeeTest(string email, string pass, string? role)
        {
            //Arrange   
            //if inline role is null, don't pass to Employee new()
            Employee test = role==null?new Employee(email, pass):new Employee(email, pass, role);
            role ??= "Employee";

            //Act
            var actual = test.ToString();

            //Assert
            var expected = $"Email: {email}\tRole: {role}\r\n";
            Assert.Equal(expected, actual);
        }


    }
}

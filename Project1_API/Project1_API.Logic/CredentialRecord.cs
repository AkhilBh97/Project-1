using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_API.Logic
{
    public class CredentialRecord
    {
        public Employee E { get; set; }
        public string? Pass { get; set; }

        public CredentialRecord(Employee e, string? pass)
        {
            E = e;
            Pass = pass;
        }
    }
}
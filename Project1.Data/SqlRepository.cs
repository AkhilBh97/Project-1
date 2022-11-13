using Project1.Logic;
using System;
using System.Collections.Generic;
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
        public Ticket CreateNewTicket()
        {
            //TODO 
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> getAllTickets()
        {
            throw new NotImplementedException();
        }

        public string getEmplEmail(int ID)
        {
            throw new NotImplementedException();
        }
    }
}

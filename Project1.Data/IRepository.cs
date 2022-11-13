using Project1.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project1.Data
{
    public interface IRepository
    {
        //Some enumerable collection of tickets
        IEnumerable<Ticket> getAllTickets();
        Ticket CreateNewTicket();
        string getEmplEmail(int ID);
    }
}

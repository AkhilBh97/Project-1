using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Client.App
{
    //Menu object will simply return a series of menus. Moving to a new menu will clear the console
    //  before displaying
    public  class Menu
    {
        private readonly string borders;
        //this represents the current menu state
        //If the menu is changed, state will reflect that
        public string Current { get; set; }

        public Menu() {
            Current = "Main";
            borders = new string('-', 40); 
        }



        //the first menu users see
        public ConsoleKey MainMenu()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\tPress the corresponding key from the list below:");
            sb.AppendLine(borders);
            sb.AppendLine("1:\tRegister a new user");
            sb.AppendLine("2:\tLogin as an Employee");
            sb.AppendLine("3:\tLogin as a Manager");
            sb.AppendLine("q:\tExit the application");
            sb.AppendLine(borders);

            return Console.ReadKey().Key;
        }

        public string RegisterMenu()
        {

        }

        public string Login
    }
}

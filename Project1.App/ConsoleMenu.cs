using Project1.Logic;
using Project1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.App
{
    //
    public class ConsoleMenu
    {
        //Fields
        private readonly string borders;
        IRepository repo;

        //Constructors
        public ConsoleMenu(IRepository repo) {
            this.repo = repo;
            borders = new string('_', 40); 
        }

        //Methods
        //Start the console menu, displays the first few options
        public void Run() {
            //clear the screen first
            Console.Clear();
            Console.WriteLine(borders);
            //tell user to press a key
            Console.WriteLine("Press the corresponding key from the list below:");
            //Option to Register
            Console.WriteLine("1:\tRegister a new user");
            //Option to log in as an Employee
            Console.WriteLine("2:\tLogin as an Employee");
            //Option to log in as a Manager
            Console.WriteLine("3:\tLogin as a Manager");
            //Option to quit
            Console.WriteLine("q:\tExit the application");
            Console.WriteLine(borders);

            //input
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.D1:
                    //Move to Register menu
                    RegisterMenu();
                    break;
                case ConsoleKey.D2:
                    //move to Employee Login
                    EmployeeLogin();
                    break;
                case ConsoleKey.D3:
                    //move to Manager Login
                    ManagerLogin();
                    break;
                case ConsoleKey.Q:
                    //Exit program
                    Console.Clear();
                    Console.WriteLine("\nPress any key to exit...");
                    return;
                default:
                    Console.WriteLine("Invalid input. Returning to main menu...");
                    //Run();
                    break;
            }
            Run();

        }

        //Now for the different menus based on user's input
        //User wants to register a new account
        private void RegisterMenu()
        {
            Console.Clear();
            Console.WriteLine("Now in the Register menu. Going back now...");
            Console.ReadKey();
            return;
            //Have user enter an email


            //Check that the email is not in use


            //Have the user enter a password


            //Have the user enter Y or N if they are a manager or not

            
            //
        }

        private void EmployeeLogin()
        {
            Console.Clear();
            Console.WriteLine("Now in Employee Login. Press any key to return now...");
            Console.ReadKey();
            return;
        }

        private void ManagerLogin()
        {
            Console.Clear();
            Console.WriteLine("Now in Manager Login. Press any key to return now...");
            Console.ReadKey();
            return;
        }
    }
}

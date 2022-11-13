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

            Console.WriteLine("\tPress the corresponding key from the list below:");
            Console.WriteLine(borders);
            //tell user to press a key
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
                    //move to Employee Login/Menu
                    EmployeeMenu();
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
        public void RegisterMenu()
        {
            Console.Clear();
            
            Console.WriteLine("Now in the Register menu. Going back now...");
            Console.WriteLine(borders);
            Console.ReadKey();
            return;
            //Have user enter an email


            //Check that the email is not in use


            //Have the user enter a password


            //Have the user enter Y or N if they are a manager or not

            
            //Create a new Employee in the repo


            //Move to Employee menu to login
        }

        public void EmployeeMenu()
        {
            Console.Clear();
            Console.WriteLine("Now in Employee Menu. Press any key to return now...");
            Console.ReadKey();
            return;
            //Enter credentials, make sure they're valid for an existing employee

            //Display the Employee menu
            //Submit a ticket, view employee's ticket history, return
            //Submit Ticket -> Ticket Submission Form
            //  On successful submission, show ticket details and restart Employee menu
            //View history -> Get All Tickets matching the employee's email
            //Return -> return to main menu
        }

        public void ManagerLogin()
        {
            Console.Clear();
            Console.WriteLine("Now in Manager Menu. Press any key to return now...");
            Console.ReadKey();
            return;
            //Enter credentials, make sure the user has a Manager role

            //Display the Manager menu
            //Process pending tickets, return
            //Process Tickets -> Process Tickets Form (show Ticket info, MUST Approve/Reject)
            //  Queue<Ticket> pending = new Queue<Ticket>(repo.getAllTickets());
            //Return -> return to main menu
        }
    }
}

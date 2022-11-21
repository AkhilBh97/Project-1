
using Project1_Client.Logic;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Project1_Client.App
{
    public class Program
    {
        static HttpClient client = new HttpClient();

        public static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        //This is the main program logic
        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://localhost:7207/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                string? userinput, pass;
                Employee e;
                //ConsoleKey choice;

                //Demonstrate registering a user
                
                e = new();
                Console.WriteLine("You will create an employee here:");
                Console.Write("Enter an email: ");
                e.Email = Console.ReadLine();
                Console.Write("\nEnter a password: ");
                pass = Console.ReadLine();
                while (pass != null)
                {
                    Console.Write("\nPassword cannot be empty. Enter a password: ";
                    pass = Console.ReadLine();
                }
                Console.Write("\nAre you a manager? (Y/N)");
                userinput = Console.ReadLine();
                if (userinput.ToUpper() == "Y") e.Role = "Manager";
                
                var url = await CreateEmployeeAsync(e, pass);
                
                
            }//end of try block
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//end of catch block
        }

        //User input for registering an employee
        

        //Show a ticket's details
        static void ShowTicket(Ticket t)
        {
            Console.WriteLine(t.ToString());
        }


        //POST methods
        //Create an employee 
        static async Task<Uri> CreateEmployeeAsync(Employee e, string pass)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync<CredentialRecord>("register", new CredentialRecord(e, pass));
            if (!response.IsSuccessStatusCode) return null;
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        //Login
        //
        static async Task<Uri> LoginAsync(Employee e, string pass)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync<CredentialRecord>("login", new CredentialRecord(e, pass));
            if (!response.IsSuccessStatusCode) return null;
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        static async Task<Uri> CreateTicketAsync(Ticket t, int emplid)
        {
            //pass a ticket object and an employee ID to create a ticket record, which is passed
            //  through the request body as a JSON object
            HttpResponseMessage response = await client.PostAsJsonAsync<TicketRecord>($"Employee/{emplid}/tickets", new TicketRecord(t, emplid));

            //no ticket submitted means a null URI return
            if (!response.IsSuccessStatusCode) return null;
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        //READ | GET methods

        //Get enumerables of tickets
        //A queue of pending tickets
        static async Task<Queue<Ticket>> GetPendingTickets()
        {
            Queue<Ticket> tickets = new Queue<Ticket>();
            string status = "Pending"; 
            string path = $"Manager/tickets/{status}";

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                tickets = await response.Content.ReadAsAsync<Queue<Ticket>>();
            }
            return tickets;
        }

        //A list of an employee's tickets, optionally filtered by status
        static async Task<List<Ticket>> GetEmplTickets(Employee e, string? status)
        {
            List<Ticket> tickets = new List<Ticket>();
            string path = $"Employee/{e.Id}/tickets";

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                tickets = await response.Content.ReadAsAsync<List<Ticket>>();
            }
            return tickets;
        }

        //UPDATE methods


    }
}
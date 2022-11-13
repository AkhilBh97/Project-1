

using Project1.Data;

namespace Project1.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionstring = File.ReadAllText(@"../../../../connection.txt");
            IRepository repo = new SqlRepository(connectionstring);
            ConsoleMenu cm = new(repo);

            cm.Run();

            Console.ReadKey();
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_4
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionString connectionString1 = new ConnectionString
            {
                DatabaseName = "Database1",
                Host = "lovalhost",
                Password = "5555",
                UserName = "User1"
            };
            ConnectionString connectionString2 = new ConnectionString
            {
                DatabaseName = "Database2",
                Host = "lovalhost",
                Password = "4444",
                UserName = "User2"
            };

            List < ConnectionString > connectionString = new List<ConnectionString>();
            connectionString.Add(connectionString1);
            connectionString.Add(connectionString2);

            CacheProvider cacheProvider = new CacheProvider();
            cacheProvider.CacheConnections(connectionString);

            List<ConnectionString> connections = cacheProvider.GetConnectionFromCache();

            foreach (var connection in connections)
            {
                Console.WriteLine($"{connection.Host} {connection.UserName} {connection.DatabaseName} {connection.Password}");
            }
            Console.ReadKey();
        }
    }
}

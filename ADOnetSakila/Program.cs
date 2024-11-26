using Microsoft.Data.SqlClient;

namespace ADOnetSakila
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input actor first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Input actor last name: ");
            string lastName = Console.ReadLine();

            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            var connection = new SqlConnection(connectionString);
            var query = new SqlCommand
                (
                "SELECT film.title " +
                "FROM actor " +
                "INNER JOIN film_actor ON actor.actor_id = film_actor.actor_id " +
                "INNER JOIN film ON film_actor.film_id = film.film_id " +
                $"WHERE actor.first_name = '{firstName}' AND actor.last_name = '{lastName}';",
                connection
                );
            
            connection.Open();
            var queryResult = query.ExecuteReader();
            Console.Clear();
            if (queryResult.HasRows)
            {
                Console.WriteLine($"Films starring {firstName} {lastName}:");
                Console.WriteLine("--------------------");
                while (queryResult.Read())
                {
                    Console.WriteLine(queryResult[0]);
                }
            }
            else
            {
                Console.WriteLine("No results with given actor name.");
            }
            connection.Close();








        }
    }
}

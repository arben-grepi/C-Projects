using MySqlConnector;
using System.Configuration;

namespace SQLInjDemoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string connectionString = "Server=127.0.0.1;Port=3306;Database=sqlinjdemo;User ID=opiskelija;Pwd=opiskelija1;";
            string connectionString = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //salataan app config
            if (!config.ConnectionStrings.SectionInformation.IsProtected)
            {
                config.ConnectionStrings.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
            }


            Console.WriteLine("Anna  käyttäjätunnus: ");
            string username = Console.ReadLine();

            Console.WriteLine("Anna salasana: ");
            string password = Console.ReadLine();

            //string sql = $"SELECT * FROM logins WHERE username = '{username}' AND password = '{password}'";
            string sql = $"SELECT * FROM logins WHERE username = @username AND password = @password";

            //Console.WriteLine("Sql: "+ sql);

            using(MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue ("password", password);

                try
                {
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        Console.WriteLine();
                        Console.WriteLine("Käyttäjätunnus ja salasana OK");
                        Console.WriteLine("Tervetuloa " + username);

                    }
                    else { Console.WriteLine("Ei löytynyt käyttäjää"); }


                }
                catch (Exception ex)
                {

                    Console.WriteLine("Virhe!");
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                }

               

            }


        }
    }
}

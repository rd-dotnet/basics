using System;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ConsoleApp12
{
    public static class ExecuteScalarExample
    {
        public static void ShowExample()
        {
            //var connectionString = "Data Source=(local);Initial Catalog=NorthWind;Integrated Security=True";
            string connectionString = ConfigurationManager
                .ConnectionStrings["NorthWindConnectionString"]
                .ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Инициализируем объект команды
                //var command = new SqlCommand();
                //command.Connection = connection;
                //var command = new OleDbCommand();
                //command.Connection = connection;
                using (var command = connection.CreateCommand())
                {
                    // заполняем объект команды данными для запроса
                    command.CommandText = "SELECT @@VERSION";

                    // выполняем запрос
                    var result = command.ExecuteScalar();

                    // обрабатываем результат
                    Console.WriteLine($"Результат выполнения:\n {result}");
                }
                connection.Close();
            }
        }
    }
}

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ConsoleApp12
{
    public static class ExecuteNonQueryExample
    {
        public static void ShowExample()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NorthWindConnectionString"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Инициализируем объект команды
                using (var command = connection.CreateCommand())
                {
                    // заполняем объект команды данными для запроса
                    command.CommandText = @"DELETE FROM Region
                                            WHERE RegionID = 5";

                    // выполняем запрос
                    var affectedRows = command.ExecuteNonQuery();

                    // обрабатываем результат
                    Console.WriteLine($"Количество удалённых строк: {affectedRows}");
                }

                connection.Close();
            }
        }
    }
}

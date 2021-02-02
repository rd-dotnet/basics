using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ConsoleApp12
{
    internal static class ExecuteReaderExample
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
                    command.CommandText = @"SELECT OrderID, OrderDate, Freight, ShipCountry
                                            FROM Orders";
                    // выполняем запрос
                    using (var reader = command.ExecuteReader())
                    {
                        // убеждаемся, что получили какие-то строки
                        if (reader.HasRows)
                        {
                            // обрабатываем результат
                            while (reader.Read())
                            {
                                int orderId = reader.GetInt32(0);
                                DateTime orderDate = reader.GetDateTime(1);
                                decimal freight = reader.GetDecimal(2);
                                string shipCountry = reader.GetString(3);
                                //decimal freight = reader.GetDecimal(3);
                                //string shipCountry = reader.GetString(2);

                                Console.WriteLine($"Order: {orderId} \n OrderDate: {orderDate} \n Freight: {freight} \n ShipCountry: {shipCountry}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Не получили строки из источника данных!");
                        }

                        // для выполнения множественных SELECT 
                        // reader.NextResult(); 
                    }
                }

                connection.Close();
            }
        }
    }
}

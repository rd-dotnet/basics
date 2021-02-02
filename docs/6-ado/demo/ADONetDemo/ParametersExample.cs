using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    internal static class ParametersExample
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
                    //var filterDate = Console.ReadLine();

                    var filterDate = "1998-05-01'; DELETE FROM Region WHERE RegionID = 5--";

                    // заполняем объект команды данными для запроса
                    command.CommandText = $@"SELECT OrderID, OrderDate, Freight, ShipCountry
                                            FROM Orders
                                            WHERE OrderDate > @date";

                    command.Parameters.AddWithValue("@date", filterDate);

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

                                Console.WriteLine($"Order: {orderId} \n OrderDate: {orderDate} \n Freight: {freight} \n ShipCountry: {shipCountry}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Не получили строки из источника данных!");
                        }
                    }
                }

                connection.Close();
            }
        }
    }
}

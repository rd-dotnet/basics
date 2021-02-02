using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp12
{
    internal static class CommandAsStoredProcedureExample
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
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "TestStoredProcedure";

                    // 1. создание и заполнение входного параметра
                    var inputParameter = command.CreateParameter();
                    inputParameter.ParameterName = "@inputParam";
                    inputParameter.Value = 1;
                    inputParameter.Direction = ParameterDirection.Input;
                    inputParameter.DbType = DbType.Int32;

                    // 2. создание и заполнение выходного параметра
                    var outParameter = command.CreateParameter();
                    outParameter.ParameterName = "@outParam";
                    outParameter.Direction = ParameterDirection.Output;
                    outParameter.DbType = DbType.Binary;
                    outParameter.Size = 8000;

                    command.Parameters.Add(inputParameter);
                    command.Parameters.Add(outParameter);

                    // убеждаемся, что выходной параметр пустой
                    Console.WriteLine(outParameter.Value ?? "empty");

                    // выполняем запрос
                    command.ExecuteNonQuery();

                    // убеждаемся, что выходной параметр не пустой
                    Console.WriteLine(outParameter.Value != null ? "not empty" : "empty");
                }

                connection.Close();
            }
        }
    }
}

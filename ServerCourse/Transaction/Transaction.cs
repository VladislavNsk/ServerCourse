using System;

using Microsoft.Data.SqlClient;

namespace Transaction
{
    internal static class Transaction
    {
        private static void Main()
        {
            var connectionString = "Data Source=.;Initial Catalog=Shop;Integrated Security=true;";
            InsertCategoryUsingTransaction(connectionString);
            InsertCategoryWithoutTransaction(connectionString);
        }

        private static void InsertCategoryUsingTransaction(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                var query1 = "INSERT INTO [dbo].[Categories] (Name) " +
                                   "VALUES (N'Телевизоры')";
                var command1 = new SqlCommand(query1, connection)
                {
                    Transaction = transaction
                };

                command1.ExecuteNonQuery();
                throw new Exception();

                transaction.Commit();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка, транзакция откачена");
                transaction.Rollback();
            }

            var query2 = "SELECT Name " +
                               "FROM [dbo].[Categories]";
            var command2 = new SqlCommand(query2, connection);
            var reader = command2.ExecuteReader();

            Console.WriteLine("Категории:");
            while (reader.Read())
            {
                Console.WriteLine(reader["Name"]);
            }

            Console.WriteLine();
        }

        private static void InsertCategoryWithoutTransaction(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            try
            {
                var query1 = "INSERT INTO [dbo].[Categories] (Name) " +
                                   "VALUES (N'Телефоны')";
                var command1 = new SqlCommand(query1, connection);

                command1.ExecuteNonQuery();
                throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла ошибка");
            }

            var query2 = "SELECT Name " +
                               "FROM [dbo].[Categories]";
            var command2 = new SqlCommand(query2, connection);
            var reader = command2.ExecuteReader();

            Console.WriteLine("Категории:");
            while (reader.Read())
            {
                Console.WriteLine(reader["Name"]);
            }

            Console.WriteLine();
        }
    }
}

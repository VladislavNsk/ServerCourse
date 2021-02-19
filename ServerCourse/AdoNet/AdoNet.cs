using System;
using System.Collections.Generic;
using System.Data;

using Microsoft.Data.SqlClient;

namespace AdoNet
{
    internal static class AdoNet
    {
        private static void Main()
        {
            var connectionString = "Data Source=.;Initial Catalog=Shop;Integrated Security=true;";

            InsertProduct(connectionString);
            InsertCategory(connectionString);
            UpdateProduct(connectionString, 1025m, 3);
            DeleteProduct(connectionString, 1);
            PrintProductsUsingReader(connectionString);
            PrintProductsUsingDataSet(connectionString);

        }

        private static void PrintProductsCount(string connectionString)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                var query = "SELECT COUNT(*) " +
                            "FROM [dbo].[Products]";
                using var command = new SqlCommand(query, connection);

                Console.WriteLine("Число продуктов = " + (int)command.ExecuteScalar());
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void InsertProduct(string connectionString)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                var queryCategoriesNames = "SELECT Name " +
                                                 "FROM [dbo].[Categories]";
                using var command1 = new SqlCommand(queryCategoriesNames, connection);
                var reader = command1.ExecuteReader();
                var categoriesNames = new List<string>();

                while (reader.Read())
                {
                    categoriesNames.Add((string)reader["Name"]);
                }

                reader.Dispose();

                Console.WriteLine("Введите имя нового продукта");
                var productName = Console.ReadLine();

                Console.WriteLine("Введите цену нового продукта");
                var price = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine("Введите категорию нового продукта");
                Console.Write($"Имеющиеся категории: ");

                foreach (var name in categoriesNames)
                {
                    Console.Write($"{name}({categoriesNames.IndexOf(name) + 1}) ");
                }

                Console.WriteLine();
                var categoryId = Convert.ToInt32(Console.ReadLine());
                var query = "INSERT INTO [dbo].[Products] (Name, Price, CategoryId) " +
                                  "VALUES(@productName, @price, @categoryId)";

                using var command2 = new SqlCommand(query, connection);
                command2.Parameters.AddRange(new SqlParameter[]
                {
                new SqlParameter("@productName", productName)
                {
                    SqlDbType = SqlDbType.NVarChar
                },
                new SqlParameter("@price", price)
                {
                    SqlDbType = SqlDbType.Decimal
                },
                new SqlParameter("@categoryId", categoryId)
                {
                    SqlDbType = SqlDbType.Int
                }
                });

                command2.ExecuteNonQuery();
                Console.WriteLine($"Добавлен новый продукт: {productName}");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void InsertCategory(string connectionString)
        {
            try
            {
                Console.WriteLine("Введите имя категории");
                var categoryName = Console.ReadLine();

                using var connection = new SqlConnection(connectionString);
                connection.Open();

                var query = "INSERT INTO [dbo].[Categories] (Name) " +
                            $"VALUES(@categoryName)";
                using var command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@categoryName", categoryName)
                {
                    SqlDbType = SqlDbType.NVarChar
                });

                command.ExecuteNonQuery();
                Console.WriteLine($"Добавлена новая категория: {categoryName}");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void UpdateProduct(string connectionString, decimal price, int productId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                var query = "UPDATE [dbo].[Products] " +
                            $"SET Price = {price} " +
                            $"WHERE Id = {productId}";
                using var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                var rowsCount = command.ExecuteNonQuery();

                Console.WriteLine(rowsCount == 0 ? $"Продукт с id ({productId}) не найден, обновление не возможно" : "Продукт обновлен");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void DeleteProduct(string connectionString, int productId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                var query = "DELETE [dbo].[Products] " +
                            $"WHERE Id = {productId}";
                using var command = new SqlCommand(query, connection);
                var rowsCount = command.ExecuteNonQuery();

                Console.WriteLine(rowsCount == 0 ? $"Продукт с id ({productId}) не найден, удаление не возможно" : "Продукт удален");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void PrintProductsUsingReader(string connectionString)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                var query = "SELECT Products.Name as ProductName, Products.Price, Categories.Name as Category " +
                            "FROM [dbo].[Categories], [dbo].[Products] " +
                            "WHERE Products.CategoryId = Categories.Id";
                using var command = new SqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Продукт: {reader.GetString("ProductName")}, " +
                                      $"цена: {reader.GetDecimal("Price")}, " +
                                      $"категория: {reader.GetString("Category")}");
                }

                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void PrintProductsUsingDataSet(string connectionString)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                var query = "SELECT Products.Name as ProductName, Products.Price, Categories.Name as Category " +
                            "FROM [dbo].[Categories], [dbo].[Products] " +
                            "WHERE Products.CategoryId = Categories.Id";
                var adapter = new SqlDataAdapter(query, connection);
                var dataSet = new DataSet();

                adapter.Fill(dataSet);
                var rows = dataSet.Tables[0].Rows;

                foreach (DataRow row in rows)
                {
                    var cells = row.ItemArray;

                    foreach (var cell in cells)
                    {
                        Console.Write($"{cell}  ");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

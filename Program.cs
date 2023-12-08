using System;
using System.Data.SqlClient;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FirmaKantstover;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Підключення до бази даних успішне!");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Помилка підключення до бази даних: {0}", e.Message);
            }

            Console.WriteLine("Виберіть завдання:");
            Console.WriteLine("1. Відобразити всю інформацію про канцтовари");
            Console.WriteLine("2. Відобразити всі типи канцтоварів");
            Console.WriteLine("3. Відобразити всіх менеджерів з продажу");
            Console.WriteLine("4. Показати канцтовари з максимальною кількістю одиниць");
            Console.WriteLine("5. Показати канцтовари з мінімальною кількістю одиниць");
            Console.WriteLine("6. Показати канцтовари з мінімальною собівартістю одиниці");
            Console.WriteLine("7. Показати канцтовари з максимальною собівартістю одиниці");
            Console.WriteLine("8. Ввести дані про канцтовар");
            Console.WriteLine("0. Вихід");

            int choice = int.Parse(Console.ReadLine());
            do
            {
                switch (choice)
                {
                    case 1:
                        var cmd = new SqlCommand("SELECT * FROM [Товари]", connection);
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Назва: {0}, Тип: {1}, Кількість: {2}, Менеджер: {3}, Собівартість: {4}", reader["Назва"], reader["Тип"], reader["Кількість"], reader["Менеджер"], reader["Собівартість"]);
                        }
                        reader.Close();
                        break;
                    case 2:
                        cmd = new SqlCommand("SELECT DISTINCT [Тип] FROM [Товари]", connection);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Тип: {0}", reader["Тип"]);
                        }
                        reader.Close();
                        break;
                    case 3:
                        cmd = new SqlCommand("SELECT DISTINCT [Менеджер] FROM [Товари]", connection);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Менеджер: {0}", reader["Менеджер"]);
                        }
                        reader.Close();
                        break;
                    case 4:
                        cmd = new SqlCommand("SELECT * FROM [Товари] ORDER BY [Кількість] DESC", connection);
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Канцтовар з максимальною кількістю одиниць:");
                        Console.WriteLine("Назва: {0}, Тип: {1}, Кількість: {2}, Менеджер: {3}, Собівартість: {4}", reader["Назва"], reader["Тип"], reader["Кількість"], reader["Менеджер"], reader["Собівартість"]);
                        reader.Close();
                        break;
                    case 5:
                        cmd = new SqlCommand("SELECT * FROM [Товари] ORDER BY [Кількість] ASC", connection);
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Канцтовар з мінімальною кількістю одиниць:");
                        Console.WriteLine("Назва: {0}, Тип: {1}, Кількість: {2}, Менеджер: {3}, Собівартість: {4}", reader["Назва"], reader["Тип"], reader["Кількість"], reader["Менеджер"], reader["Собівартість"]);
                        reader.Close();
                        break;
                    case 6:
                        cmd = new SqlCommand("SELECT * FROM [Товари] ORDER BY [Собівартість] ASC", connection);
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Канцтовар з мінімальною собівартістю одиниці:");
                        Console.WriteLine("Назва: {0}, Тип: {1}, Кількість: {2}, Менеджер: {3}, Собівартість: {4}", reader["Назва"], reader["Тип"], reader["Кількість"], reader["Менеджер"], reader["Собівартість"]);
                        reader.Close();
                        break;

                    case 7:
                        cmd = new SqlCommand("SELECT * FROM [Товари] ORDER BY [Собівартість] DESC", connection);
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Канцтовар з максимальною собівартістю одиниці:");
                        Console.WriteLine("Назва: {0}, Тип: {1}, Кількість: {2}, Менеджер: {3}, Собівартість: {4}", reader["Назва"], reader["Тип"], reader["Кількість"], reader["Менеджер"], reader["Собівартість"]);
                        reader.Close();
                        break;

                    case 8:
                        Console.WriteLine("Введіть назву канцтовару:");
                        string name = Console.ReadLine();

                        Console.WriteLine("Введіть тип канцтовару:");
                        string type = Console.ReadLine();

                        Console.WriteLine("Введіть кількість канцтоварів:");
                        int quantity = int.Parse(Console.ReadLine());

                        Console.WriteLine("Введіть ім'я менеджера:");
                        string manager = Console.ReadLine();

                        Console.WriteLine("Введіть собівартість одиниці канцтовару:");
                        float cost = float.Parse(Console.ReadLine());

                        cmd = new SqlCommand("INSERT INTO [Товари] ([Назва], [Тип], [Кількість], [Менеджер], [Собівартість]) VALUES (@name, @type, @quantity, @manager, @cost)", connection);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@manager", manager);
                        cmd.Parameters.AddWithValue("@cost", cost);
                        cmd.ExecuteNonQuery();

                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Назва: {0}, Тип: {1}, Кількість: {2}, Менеджер: {3}, Собівартість: {4}", reader["Назва"], reader["Тип"], reader["Кількість"], reader["Менеджер"], reader["Собівартість"]);
                        }
                        reader.Close();
                        break;
                }
            }while(choice != 0 );
        }
    }
}

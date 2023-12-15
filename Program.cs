using System;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
{
    internal class Program
    {
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader reader;

        static string connectionString = "Server=DESKTOP-EBTO5CT;Database=LibraryDB;trusted_connection=true;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Library Management System");
                Console.WriteLine("1. Display Book Inventory");
                Console.WriteLine("2. Add New Book");
                Console.WriteLine("3. Update Book Quantity");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        DisplayBookInventory();
                        break;
                    case 2:
                        AddNewBook();
                        break;
                    case 3:
                        UpdateBookQuantity();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void DisplayBookInventory()
        {
            DataSet dataSet = RetrieveData();

            Console.WriteLine("Book Inventory:");
            foreach (DataRow row in dataSet.Tables["Books"].Rows)
            {
                Console.WriteLine($"Book ID: {row["BookId"]}, Title: {row["Title"]}, Author: {row["Author"]}, Genre: {row["Genre"]}, Quantity: {row["Quantity"]}");
            }
        }

        static void AddNewBook()
        {
            try
            {
                Console.WriteLine("Enter BookId");
                int BookId = int.Parse(Console.ReadLine());
                Console.Write("Enter Book Title: ");
                string title = Console.ReadLine();

                Console.Write("Enter Author: ");
                string author = Console.ReadLine();

                Console.Write("Enter Genre: ");
                string genre = Console.ReadLine();

                Console.Write("Enter Quantity: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Books (BookId, Title, Author, Genre, Quantity) VALUES (@BookId, @Title, @Author, @Genre, @Quantity)", connection))
                    {
                        cmd.Parameters.AddWithValue("@BookId", BookId);
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Author", author);
                        cmd.Parameters.AddWithValue("@Genre", genre);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);

                        cmd.ExecuteNonQuery();

                        Console.WriteLine("New Book Added Successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void UpdateBookQuantity()
        {
            Console.Write("Enter Book ID to update quantity: ");
            int bookId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter New Quantity: ");
            int newQuantity = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE Books SET Quantity = @Quantity WHERE BookId = @BookId", connection))
                {
                    cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Book Quantity Updated Successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"No book found with ID {bookId}.");
                    }
                }
            }
        }

        static DataSet RetrieveData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Books", connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Books");

                return dataSet;
            }
        }
    }
}




using Microsoft.Data.SqlClient;
using System.Data;

namespace Connecting_DB
{
    internal class Program
    {

        
        SqlConnection connection = new SqlConnection("Server=LAPTOP-JIQ20LLJ\\ROOT;Integrated Security=true;Initial Catalog=northwind;TrustServerCertificate=True");
        public Program()
        {
           
        }
        void GetProductDeatilsFromDatabase()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select * from Products";

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Name: \t{reader["ProductName"]}");
                    Console.WriteLine($"Price: \t${reader["UnitPrice"]}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                connection.Close();
            }
        }
        void CreateUser()
        {
            string username, password;
            Console.WriteLine("Enter username: ");
            username = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            password = Console.ReadLine();
            string insertQuery = $"Insert into users values('{username}','{password}')";
            SqlCommand cmd = new SqlCommand(insertQuery, connection);

            try
            {
                connection.Open();
                int rowsEffected = cmd.ExecuteNonQuery();
                if (rowsEffected > 0)
                {
                    Console.WriteLine("User added");
                }
                else
                {
                    Console.WriteLine("not added");
                }
            }
            catch (Exception ex) {
              Console.WriteLine (ex.Message);
            }
            finally { 
                connection.Close(); 
            }
        
        }

        bool CheckUser(string username, string password)
        {
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM Users WHERE Username=@un AND Password=@pass", connection);
            try
            {
                connection.Open();
                sqlCommand.Parameters.AddWithValue("@un", username);
                sqlCommand.Parameters.AddWithValue("@pass", password);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }
        void UpdatePassword()
        {
            string username, password, newPassword;
            Console.WriteLine("Please enter the username");
            username = Console.ReadLine();
            Console.WriteLine("Pleae enter your current password");
            password = Console.ReadLine();
            try
            {
                if (CheckUser(username, password))
                {
                    Console.WriteLine("Please enter your new password");
                    newPassword = Console.ReadLine();
                    SqlCommand sqlCommand = new SqlCommand("UPDATE Users SET Password=@newpass WHERE Username=@un", connection);
                    sqlCommand.Parameters.AddWithValue("@newpass", newPassword);
                    sqlCommand.Parameters.AddWithValue("@un", username);
                    try
                    {
                        connection.Open();
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Password updated successfully");
                        }
                        else
                        {
                            Console.WriteLine("Password update failed");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Could not verify user. Sorry cannot update password now.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        void UnderstandingDisconnectedArchitecture()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("SELECT * FROM Products", connection);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                Console.WriteLine($"The current connection state is {connection.State}");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"Name: {row["ProductName"]}");
                    Console.WriteLine($"Price: {row["UnitPrice"]}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        void GetFromCategories()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("SELECT * FROM categories", connection);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                Console.WriteLine($"The current connection state is {connection.State}");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"Name: {row["CategoryName"]}");
                    Console.WriteLine($"Description: {row["Description"]}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        
        void DeleteUser()
        {
            Console.WriteLine("Enter user name");
            string username = Console.ReadLine();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand($"delete FROM users where username ='{username}' ", connection);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                Console.WriteLine($"The current connection state is {connection.State}");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"Name: {row["CategoryName"]}");
                    Console.WriteLine($"Description: {row["Description"]}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        static void Main(string[] args)
        {
            Program program = new Program();
            //program.CreateUser();
            //program.GetFromCategories();
            //program.UpdatePassword();
            program.DeleteUser();
            Console.WriteLine("Hello, World!");
        }
    }
}

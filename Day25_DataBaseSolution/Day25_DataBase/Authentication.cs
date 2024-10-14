using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25_DataBase
{
    internal class Authentication:IAuthentication
    {
        SqlConnection conn = new SqlConnection("Server=LAPTOP-JIQ20LLJ\\ROOT;Integrated Security=true;Initial Catalog=northwind;TrustServerCertificate=True");

        User user;
        public Authentication()
        {
            user = new User();
        }      

        private void getUserDetails()
        {
            Console.WriteLine("Enter user name");
            string username = Console.ReadLine();
            user.UserName = username;
            Console.WriteLine("set user password");
            string password = Console.ReadLine();
            user.Password = password;      
        }
        private Boolean checkUser(string username, string password)
        {
            SqlCommand cmd = new SqlCommand($"SELECT * FROM Users WHERE username=@un AND password=@pass", conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@un", username);
                cmd.Parameters.AddWithValue("@pass", password);
                SqlDataReader reader = cmd.ExecuteReader();
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
                conn.Close();
            }
        }
        public void register()
        {
            getUserDetails();
            if (!checkUser(user.UserName, user.Password))
            {

                string insertQuery = $"Insert into users values(@un,@pass)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                try
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@un", user.UserName);
                    cmd.Parameters.AddWithValue("@pass", user.Password);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Registration successfull login now");
                    }
                    else
                    {
                        Console.WriteLine("Registration failed");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                finally
                {
                    conn.Close();
                }
            }
            else
            {
                Console.WriteLine("User already exist please try loging in");
            }

        }

        public Boolean login()
        {
            getUserDetails();
            try {
                if (checkUser(user.UserName, user.Password))
                {
                    Console.WriteLine("Login succesfull");
                    return true;
                }
                
                Console.WriteLine("User does not exist register first");
                return false;
                
            }
            catch(Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
           
        }



        public void updatePassword()
        {
            getUserDetails();
            if (checkUser(user.UserName, user.Password))
            {
                Console.WriteLine("Enter new password");
                string newPassword = Console.ReadLine();
                string insertQuery = $"update users set password = @pass where username=@un";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                try
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@un", user.UserName);
                    cmd.Parameters.AddWithValue("@pass", user.Password);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Update successfull");
                    }
                    else
                    {
                        Console.WriteLine("Update failed");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                finally
                {
                    conn.Close();
                }
            }
            else
            {
                Console.WriteLine("User does not exist");
            }

        }
        
    }
}

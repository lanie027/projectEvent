using LoanManagementSystem.module;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.controller
{
    internal class function_login_checker
    {
        private pattern_singleton singletoninstance = pattern_singleton.Instance;
        public bool isLoginValid(string username, string password)
        {
            SqlConnection sqlconnectioninstance = singletoninstance.getsqlconneciton();

            Console.WriteLine($"Checking login for {username} with password {password}");

            sqlconnectioninstance.Open();

            string query = "SELECT * FROM users WHERE username = @Username AND password = @Password";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlconnectioninstance))
            {

                sqlCommand.Parameters.AddWithValue("@Username", username);
                sqlCommand.Parameters.AddWithValue("@Password", password);

                try
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        // Access columns using reader["ColumnName"]
                        string data_username = reader["username"].ToString();
                        string data_password = reader["password"].ToString();

                        // Process retrieved data (replace with your logic)
                        Console.WriteLine($"Validation ID: {data_username}, Name: {data_password}");

                        return true;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing SQL query: {ex.Message}");
                }
                finally
                {
                    // Close the connection when done
                    if (sqlconnectioninstance.State == System.Data.ConnectionState.Open)
                    {
                        sqlconnectioninstance.Close();
                    }
                }
            }

            return false;
        }

        public int loginType(string username, string password)
        {
            SqlConnection sqlConnectionInstance = singletoninstance.getsqlconneciton();

            // Query to retrieve data with parameters
            string query = "SELECT UserType FROM users WHERE username = @Username AND password = @Password";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnectionInstance))
            {
                // Use parameters to avoid SQL injection
                sqlCommand.Parameters.AddWithValue("@Username", username);
                sqlCommand.Parameters.AddWithValue("@Password", password);

                try
                {
                    object result = sqlCommand.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        // Parse the result to int (replace with your logic based on your UserType)
                        int userType = Convert.ToInt32(result);

                        Console.WriteLine($"Priority: {userType}");

                        return userType;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing SQL query: {ex.Message}");
                }
                finally
                {
                    // Close the connection when done
                    if (sqlConnectionInstance.State == System.Data.ConnectionState.Open)
                    {
                        sqlConnectionInstance.Close();
                    }
                }
            }

            // Return a default value if login fails or an error occurs
            return 0;
        }
    }
}

using LoanManagementSystem.module;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.controllerandmodel
{
    internal class function_add_validid
    {
        private pattern_singleton singletoninstance = pattern_singleton.Instance;
        public bool isLoginValid(string username, string password)
        {
            SqlConnection sqlconnectioninstance = singletoninstance.GetSqlConnection();

            Console.WriteLine($"Checking login for {username} with password {password}");

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
    }
}

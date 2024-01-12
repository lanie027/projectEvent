using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoanManagementSystem.controller;
using LoanManagementSystem.module;
using LoanManagementSystem.view;

namespace LoanManagementSystem.controllerandmodel
{
    internal class function_user_manager
    {
        private function_login_checker function_login_checker_instance = new function_login_checker();
        private function_manage_ui function_manage_ui_instance = new function_manage_ui();
        private pattern_singleton singletoninstance = pattern_singleton.Instance;
        private interface_ui_load interface_ui_load_instance = null;
        private function_register_checker function_register_checker_instance = null;

        public function_user_manager() { }

        public function_user_manager(string username, string password, Form1 form)
        {
            try
            {
                bool valid = function_login_checker_instance.isLoginValid(username, password);

                if (valid)
                {
                    int type = function_login_checker_instance.loginType(username, password);

                    switch (type)
                    {
                        case 1:
                            interface_ui_load_instance = function_manage_ui_instance.manage_ui(new ui_admin(), form);
                            break;
                        case 2:
                            interface_ui_load_instance = function_manage_ui_instance.manage_ui(new ui_backoffice(), form);
                            break;
                        case 3:
                            interface_ui_load_instance = function_manage_ui_instance.manage_ui(new ui_collector(), form);
                            break;
                        case 4:
                            interface_ui_load_instance = function_manage_ui_instance.manage_ui(new ui_borrower(), form);
                            break;
                        default:
                            // Handle unexpected user types
                            Console.WriteLine("Unexpected user type");
                            break;
                    }

                    //remove the login panel
                    interface_ui_load_instance.remove();

                    // Load the interface
                    interface_ui_load_instance.load();
                }
                else
                {
                    // Handle invalid login
                    MessageBox.Show("Wrong username or password!");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                //empty
            }
        }
        public bool user_manager(string firstname, string middlename, string lastname, string age, string address, string email, string contact, string username, string password)
        {
            function_register_checker_instance = new function_register_checker();

            bool isValid = function_register_checker_instance.isRegisterValid(firstname, middlename, lastname, age, address, email, contact, username, password);

            return isValid;
        }

        public string[] GetUserRowData(string username)
        {
            SqlConnection sqlConnectionInstance = singletoninstance.GetSqlConnection();

            // Query to retrieve data with parameters
            string query = "SELECT * FROM users WHERE username = @Username";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnectionInstance))
            {
                // Use parameters to avoid SQL injection
                sqlCommand.Parameters.AddWithValue("@Username", username);

                try
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        // Access columns using reader["ColumnName"]
                        string data_id = reader["u_id"].ToString();
                        string data_firstname = reader["fname"].ToString();
                        string data_middlename = reader["mname"].ToString();
                        string data_lastname = reader["lname"].ToString();
                        string data_age = reader["age"].ToString();
                        string data_address = reader["address"].ToString();
                        string data_email = reader["email"].ToString();
                        string data_contact = reader["contact"].ToString();
                        string data_datecreated = reader["date_created"].ToString();
                        string data_username = reader["username"].ToString();
                        string data_password = reader["password"].ToString();
                        string data_type = reader["type"].ToString();
                        // ... add other columns as needed ...

                        // Process retrieved data (replace with your logic)
                        Console.WriteLine($"Data for {data_username}: Password={data_password}");

                        // Store values in an array
                        string[] userData = new string[] { data_id, data_firstname, data_middlename, data_lastname, data_age, data_address,
                            data_email, data_contact, data_datecreated, data_username, data_password, data_type /* add other values here */ };

                        reader.Close();
                        return userData;
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
                    if (sqlConnectionInstance.State == System.Data.ConnectionState.Open)
                    {
                        sqlConnectionInstance.Close();
                    }
                }
            }

            // Return an empty array if the user is not found
            return new string[0];
        }

    }
}

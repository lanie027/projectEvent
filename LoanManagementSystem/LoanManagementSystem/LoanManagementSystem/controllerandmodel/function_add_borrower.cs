using LoanManagementSystem.module;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.controllerandmodel
{
    internal class function_add_borrower
    {
        private pattern_singleton singletoninstance = pattern_singleton.Instance;
        public bool add_borrower(string firstname, string middlename, string lastname, int age, string address, string email, string contact, string username, string password)
        {
            SqlConnection sqlconnectioninstance = singletoninstance.GetSqlConnection();

            // Assuming you have a table named Borrowers with columns corresponding to the parameters
            string query = "INSERT INTO users (fname, mname, lname, age, address, email, contact, date_created, username, password, type) VALUES (@FirstName, @MiddleName, @LastName, @Age, @Address, @Email, @Contact, @Date_Created, @Username, @Password, 1)";

            DateTime birthDate = new DateTime(1990, 1, 15);

            using (SqlCommand command = new SqlCommand("AddNewBorrower", sqlconnectioninstance))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters to the SQL command
                command.Parameters.AddWithValue("@firstname", firstname);
                command.Parameters.AddWithValue("@middlename", middlename);
                command.Parameters.AddWithValue("@lastname", lastname);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@contact", contact);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                // Use parameterized query for the date parameter
                command.Parameters.AddWithValue("@date_created", DateTime.Now);

                //Default number is 4 for borrower
                command.Parameters.AddWithValue("@type", 4);


                // Execute the query
                int rowsAffected = command.ExecuteNonQuery();

                // Close the connection after executing the command
                sqlconnectioninstance.Close();

                // Check if the query was successful
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Borrower added successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to add borrower.");
                    return false;
                }
            }
        }
    }
}

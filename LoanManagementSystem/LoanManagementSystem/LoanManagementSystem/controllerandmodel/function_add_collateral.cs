using LoanManagementSystem.module;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.controllerandmodel
{
    internal class function_add_collateral
    {
        private pattern_singleton pattern_single_instance = pattern_singleton.Instance;
        private int id;
        public function_add_collateral(string picture, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(pattern_single_instance.GetStringConnection()))
                {
                    connection.Open();

                    string query = "insert into collateral values (@id, @picture)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Set parameter values
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@picture", picture);

                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} row(s) inserted successfully.");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public int GetPictureId(string picture)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(pattern_single_instance.GetStringConnection()))
                {
                    connection.Open();

                    string query = $"SELECT collateral_id FROM collateral WHERE picture = '{picture}';";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (int.TryParse(reader["collateral_id"].ToString(), out int id))
                                {
                                    return id;
                                }
                                else
                                {
                                    // Handle the case when the conversion fails
                                    // (e.g., the value in the database is not a valid integer)
                                    Console.WriteLine("Failed to parse collateral_id.");
                                    return -1; // or another default value
                                }
                            }
                            else
                            {
                                // Handle the case when no rows are returned
                                Console.WriteLine("No collateral found for the specified picture.");
                                return -1; // or another default value
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return -1;
        }
    }
}

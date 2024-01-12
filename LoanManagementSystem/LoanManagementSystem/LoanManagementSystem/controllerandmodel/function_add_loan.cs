using LoanManagementSystem.module;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanManagementSystem.controllerandmodel
{
    internal class function_add_loan
    {
        private pattern_singleton pattern_singleton_instance = pattern_singleton.Instance;

        public void add_loan(int type, int borrower, int plan, int company, int collector, int coborrower, int compound, string collateral, double amount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(pattern_singleton_instance.GetStringConnection()))
                {
                    // Open the connection
                    connection.Open();

                    //insert colalteral
                    function_add_collateral function_Add_Collateral = new function_add_collateral(collateral, borrower);

                    //get collateral id
                    int collateralId = function_Add_Collateral.GetPictureId(collateral);

                    // Create an INSERT command with parameters
                    string insertQuery = "INSERT INTO loan_list (type_id, borrower_id, amount, plan_id, status, date_created, company_id, collector_id, collateral_id, co_borrower_id, compound_id) " +
                                         "VALUES (@typeId, @borrowerId, @amount, @planId, @status, @dateCreated, @companyId, @collectorId, @collateralId, @coBorrowerId, @compoundId)";

                    using (SqlCommand command = new SqlCommand("AddNewLoan", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Set parameter values
                        command.Parameters.AddWithValue("@typeId", type);
                        command.Parameters.AddWithValue("@borrowerId", borrower);
                        command.Parameters.AddWithValue("@amount", amount);
                        command.Parameters.AddWithValue("@planId", plan);
                        command.Parameters.AddWithValue("@status", 0);
                        command.Parameters.AddWithValue("@dateCreated", DateTime.Now);
                        command.Parameters.AddWithValue("@companyId", company);
                        
                        if(collateral.Equals(""))
                        {
                            command.Parameters.AddWithValue("@collateralId", 1);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@collateralId", collateralId);
                        }
                        if(coborrower == 0)
                        {
                            command.Parameters.AddWithValue("@coBorrowerId", 1003);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@coBorrowerId", coborrower);
                        }

                        command.Parameters.AddWithValue("@compoundId", compound);
                        command.Parameters.AddWithValue("@collectorId", collector);

                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} row(s) inserted successfully.");
                    }

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {

            }
         }
    }
}

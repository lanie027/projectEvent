using LoanManagementSystem.module;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanManagementSystem.controllerandmodel
{
    internal class function_loan_calculation
    {
        // Add the necessary helper functions for extracting values and performing calculations
        private pattern_singleton pattern_singleton_instance = pattern_singleton.Instance;
        public int GetMonthsFromFormattedString(string formattedString)
        {
            // Use regular expression to extract the number of months
            // Example: Extract '12' from '12 month/s [10%, 1%]'
            Regex regex = new Regex(@"(\d+) month/s", RegexOptions.IgnoreCase);
            Match match = regex.Match(formattedString);

            if (match.Success && match.Groups.Count > 1)
            {
                if (int.TryParse(match.Groups[1].Value, out int months))
                {
                    return months;
                }
            }

            throw new FormatException("Unable to extract months from the formatted string.");

        }

        public double GetInterestPercentageFromFormattedString(string formattedString)
        {
            // Extract the interest percentage from the formatted string
            // Example: Extract '8' from '12 month/s [8%, 3%]'
            int startIndex = formattedString.IndexOf('[') + 1;
            int endIndex = formattedString.IndexOf('%', startIndex);
            string interestPercentageString = formattedString.Substring(startIndex, endIndex - startIndex);

            if (double.TryParse(interestPercentageString, out double interestPercentage))
            {
                return interestPercentage;
            }
            else
            {
                throw new FormatException("Unable to extract interest percentage from the formatted string.");
            }
        }

        public double GetPenaltyRateFromFormattedString(string formattedString)
        {
            // Use regular expression to extract the penalty rate
            // Example: Extract '1' from '12 month/s [10%, 1%]'
            Regex regex = new Regex(@"\[(\d+(\.\d+)?)%,\s*(\d+(\.\d+)?)%]", RegexOptions.IgnoreCase);
            Match match = regex.Match(formattedString);

            if (match.Success && match.Groups.Count == 5)
            {
                if (double.TryParse(match.Groups[3].Value, out double penaltyRate))
                {
                    return penaltyRate;
                }
            }

            throw new FormatException("Unable to extract penalty rate from the formatted string.");
        }

        public double CalculateMonthlyPayableAmount(double enteredAmount, int months, double interestPercentage)
        {
            double monthlyInterestRate = interestPercentage / 100 / 12;
            double denominator = Math.Pow(1 + monthlyInterestRate, months) - 1;

            if (denominator != 0)
            {
                double monthlyPayableAmount = enteredAmount * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, months)) / denominator;
                return monthlyPayableAmount;
            }
            else
            {
                throw new DivideByZeroException("Denominator is zero. Unable to calculate monthly payable amount.");
            }
        }

        public double CalculatePenaltyAmount(double enteredAmount, double penaltyRate)
        {
            double penaltyAmount = enteredAmount * (penaltyRate / 100);
            return penaltyAmount;
        }

        public void DatabaseLoanCalculation(int month, int penalty, int interest, int rebate)
        {
            pattern_singleton pattern_singleton_instance = pattern_singleton.Instance;



            using (SqlConnection sqlConnection = new SqlConnection(pattern_singleton_instance.GetStringConnection()))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {

                }
            }
        }

        public void GetLoanCalculation()
        {
            // Set the parameters
            int listId = 1; // Replace with your list_id
            int months = 12; // Replace with the desired number of months
            decimal principalAmount = 3000.00m; // Replace with the principal amount
            decimal penaltyRate = 10.00m; // Replace with the penalty rate
            int compoundId = 2; // Replace with the desired compound frequency (2 for Monthly)

            // Create a SqlConnection
            using (SqlConnection connection = new SqlConnection())
            {
                // Open the connection
                connection.Open();

                // Create a command with the stored procedure
                using (SqlCommand command = new SqlCommand("dbo.GenerateLoanSchedules", connection))
                {
                    // Set the command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@list_id", listId);
                    command.Parameters.AddWithValue("@months", months);
                    command.Parameters.AddWithValue("@principalAmount", principalAmount);
                    command.Parameters.AddWithValue("@penaltyRate", penaltyRate);
                    command.Parameters.AddWithValue("@compoundId", compoundId);

                    try
                    {
                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        Console.WriteLine("Loan schedules generated successfully.");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error executing stored procedure: {ex.Message}");
                    }
                }
            }
        }
        public void CalculateTotalPaymentAndDisplay(int loanMonths, decimal principalAmount, decimal penaltyRate, int compoundId, Form1 form)
        {
            using (SqlConnection connection = new SqlConnection(pattern_singleton_instance.GetStringConnection()))
            {
                connection.Open();

                DateTime startDate = DateTime.Now;

                using (SqlCommand command = new SqlCommand("dbo.CalculateTotalPaymentNoPaymentDate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@loanMonths", loanMonths);
                    command.Parameters.AddWithValue("@principalAmount", principalAmount);
                    command.Parameters.AddWithValue("@penaltyRate", penaltyRate);
                    command.Parameters.AddWithValue("@compoundId", compoundId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal totalPayment = reader.GetDecimal(reader.GetOrdinal("TotalPayment"));
                            decimal compoundInterest = reader.GetDecimal(reader.GetOrdinal("CompoundInterest"));
                            decimal penaltyRateResult = reader.GetDecimal(reader.GetOrdinal("PenaltyRate"));

                            Console.WriteLine($"Total Payment: {totalPayment:C}");
                            Console.WriteLine($"Compound Interest: {compoundInterest:C}");
                            Console.WriteLine($"Penalty Rate: {penaltyRateResult:C}");

                            form.label_dashboard_loanlist_monthlypayableamount.Text = compoundInterest.ToString();
                            form.label_dashboard_loanlist_penaltyamount.Text = penaltyRateResult.ToString();
                            form.label_dashboard_loanlist_totalpayableamount.Text = totalPayment.ToString();
                        }
                    }
                }
            }
        }



    }
}

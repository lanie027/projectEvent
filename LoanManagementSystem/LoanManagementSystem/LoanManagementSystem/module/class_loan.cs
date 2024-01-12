using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanManagementSystem.module
{
    internal class class_loan
    {
        int loan_id;
        int borrower_id;
        int coborrower_id;
        int type_id;
        int compound_id;
        int plan_id;
        int company_id;
        int collector_id;
        string collateral;

        //rates
        int penaltyrate;
        int compoundvalue;
        int interestrate;
        int rebaterate;

        //term
        int month;

        //amount
        int amount;

        //status
        int status;

        //reference
        string reference;

        public int Loan_id { get => loan_id; set => loan_id = value; }
        public int Borrower_id { get => borrower_id; set => borrower_id = value; }
        public int Coborrower_id { get => coborrower_id; set => coborrower_id = value; }
        public int Type_id { get => type_id; set => type_id = value; }
        public int Compound_id { get => compound_id; set => compound_id = value; }
        public int Plan_id { get => plan_id; set => plan_id = value; }
        public int Company_id { get => company_id; set => company_id = value; }
        public int Collector_id { get => collector_id; set => collector_id = value; }
        public string Collateral { get => collateral; set => collateral = value; }
        public int Penaltyrate { get => penaltyrate; set => penaltyrate = value; }
        public int Compoundvalue { get => compoundvalue; set => compoundvalue = value; }
        public int Interestrate { get => interestrate; set => interestrate = value; }
        public int Month { get => month; set => month = value; }
        public int Rebaterate { get => rebaterate; set => rebaterate = value; }
        public int Amount { get => amount; set => amount = value; }
        public string Reference { get => reference; set => reference = value; }
        public int Status { get => status; set => status = value; }

        public string ToString()
        {
            return $"Loan ID: {Loan_id}\nBorrower ID: {Borrower_id}\nCo-Borrower ID: {Coborrower_id}\nType ID: {Type_id}\nCompound ID: {Compound_id}\nPlan ID: {Plan_id}\nCompany ID: {Company_id}\nCollector ID: {Collector_id}\nCollateral: {Collateral}";

        }

        public void reset()
        {
            this.loan_id = 0;
            this.borrower_id = 0;
            this.coborrower_id = 0;
            this.type_id = 0;
            this.compound_id = 0;
            this.plan_id = 0;
            this.company_id = 0;
            this.collector_id = 0;
            this.collateral = "";
            this.penaltyrate = 0;
            this.compoundvalue = 0;
            this.interestrate = 0;
            this.month = 0;
            this.rebaterate = 0;
        }

        private pattern_singleton pattern_singleton_instance = pattern_singleton.Instance;

        public int GetInterestRate(int interestId)
        {
            string sqlQuery = $"select value from interest_percentage where interest_percentage_id = {interestId}";

            using (SqlConnection sqlconnection = new SqlConnection(pattern_singleton_instance.GetStringConnection()))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlconnection))
                {
                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlReader.Read())
                        {
                            if (int.TryParse(sqlReader["value"].ToString(), out int value))
                            {
                                return value;
                            }
                            else
                            {
                                // Handle the case when the conversion fails
                                // (e.g., the value in the database is not a valid integer)
                                Console.WriteLine("Failed to parse interest_percentage_id.");
                                return -1; // or another default value
                            }
                        }
                        else
                        {
                            // Handle the case when no rows are returned
                            Console.WriteLine("No interest found for the specified picture.");
                            return -1; // or another default value
                        }
                    }
                }    
            }    
        }

        public int GetPenaltyRate(int penaltyid)
        {
            string sqlQuery = $"select value from penalty_rate where penalty_rate_id = {penaltyid}";

            using (SqlConnection sqlconnection = new SqlConnection(pattern_singleton_instance.GetStringConnection()))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlconnection))
                {
                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlReader.Read())
                        {
                            if (int.TryParse(sqlReader["value"].ToString(), out int value))
                            {
                                return value;
                            }
                            else
                            {
                                // Handle the case when the conversion fails
                                // (e.g., the value in the database is not a valid integer)
                                Console.WriteLine("Failed to parse penalty_rate_id.");
                                return -1; // or another default value
                            }
                        }
                        else
                        {
                            // Handle the case when no rows are returned
                            Console.WriteLine("No interest found for the specified picture.");
                            return -1; // or another default value
                        }
                    }
                }
            }
        }

        public int GetCompoundRate(int compoundId)
        {
            string sqlQuery = $"select value from compound where compound_id = {compoundId}";

            using (SqlConnection sqlconnection = new SqlConnection(pattern_singleton_instance.GetStringConnection()))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlconnection))
                {
                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlReader.Read())
                        {
                            if (int.TryParse(sqlReader["value"].ToString(), out int value))
                            {
                                return value;
                            }
                            else
                            {
                                // Handle the case when the conversion fails
                                // (e.g., the value in the database is not a valid integer)
                                Console.WriteLine("Failed to parse penalty_rate_id.");
                                return -1; // or another default value
                            }
                        }
                        else
                        {
                            // Handle the case when no rows are returned
                            Console.WriteLine("No interest found for the specified picture.");
                            return -1; // or another default value
                        }
                    }
                }
            }
        }

        public int GetLoanList(int compoundId)
        {
            string sqlQuery = $"select value from compound where compound_id = {compoundId}";

            using (SqlConnection sqlconnection = new SqlConnection(pattern_singleton_instance.GetStringConnection()))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlconnection))
                {
                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlReader.Read())
                        {
                            if (int.TryParse(sqlReader["value"].ToString(), out int value))
                            {
                                return value;
                            }
                            else
                            {
                                // Handle the case when the conversion fails
                                // (e.g., the value in the database is not a valid integer)
                                Console.WriteLine("Failed to parse penalty_rate_id.");
                                return -1; // or another default value
                            }
                        }
                        else
                        {
                            // Handle the case when no rows are returned
                            Console.WriteLine("No interest found for the specified picture.");
                            return -1; // or another default value
                        }
                    }
                }
            }
        }



    }
}

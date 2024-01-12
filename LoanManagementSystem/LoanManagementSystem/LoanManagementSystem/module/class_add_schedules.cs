using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanManagementSystem.module
{
    internal class class_add_schedules
    {
        private pattern_singleton pattern_singleton_instance = pattern_singleton.Instance;
        public void makeschedule(int list_id, int loanMonths, int principalAmount, int penaltyRate, int compoundId)
        {
            using (SqlCommand command = new SqlCommand("dbo.GenerateLoanSchedules", new SqlConnection(pattern_singleton_instance.GetStringConnection())))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@loanMonths", loanMonths);
                command.Parameters.AddWithValue("@principalAmount", principalAmount);
                command.Parameters.AddWithValue("@penaltyRate", penaltyRate);
                command.Parameters.AddWithValue("@compoundId", compoundId);

                command.ExecuteNonQuery();
            }
        }
    }
}

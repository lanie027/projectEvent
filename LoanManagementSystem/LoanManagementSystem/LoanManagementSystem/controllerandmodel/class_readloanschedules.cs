using LoanManagementSystem.module;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanManagementSystem.controllerandmodel
{
    internal class class_readloanschedules
    {
        private pattern_singleton pattern_singleton_instance = pattern_singleton.Instance;
        private Form1 form;
        public class_readloanschedules(Form1 form)
        {
            this.form = form;
        }

        public DataTable getschedulelist()
        {
            string query = "SELECT schedule_id, date_due, CASE WHEN isPaid = 1 THEN 'Yes' ELSE 'No' END AS isPaid, FORMAT(amount, ',') AS amount FROM loan_schedules";
            using (SqlCommand command = new SqlCommand(query, new SqlConnection(pattern_singleton_instance.GetStringConnection())))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    // Create a DataTable to hold the data
                    DataTable table = new DataTable();

                    // Fill the DataTable with the retrieved data
                    adapter.Fill(table);

                    return table;
                }
            }
            return null;
        }
    }
}

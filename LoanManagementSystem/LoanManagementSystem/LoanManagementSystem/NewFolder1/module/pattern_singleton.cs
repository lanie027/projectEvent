using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;


namespace LoanManagementSystem.module
{
    internal class pattern_singleton
    {
        private static readonly Lazy<pattern_singleton> instance = new Lazy<pattern_singleton>(() => new pattern_singleton());

        private readonly SqlConnection sqlConnection;

        private pattern_singleton()
        {
            // Set your connection string here
            string connectionString = "DESKTOP-6RDUCQU\\Sasas-pc";
            sqlConnection = new SqlConnection(connectionString);
        }

        public static pattern_singleton Instance => instance.Value;

        public SqlConnection getsqlconneciton()
        {
            if (sqlConnection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    sqlConnection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error opening SQL connection: {ex.Message}");
                }
            }

            return sqlConnection;
        }
    }
}

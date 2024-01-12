using System;
using System.Collections.Generic;
using System.Data;
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
        private string connectionString = "";
        private SqlConnection sqlConnection;

        private pattern_singleton()
        {
            // Constructor - do not initialize the connection string here
        }

        public static pattern_singleton Instance => instance.Value;

        public SqlConnection GetSqlConnection()
        {
            if (sqlConnection == null)
            {
                // Set your connection string here
                connectionString = "Data Source=DESKTOP-KOTS3GS\\SQLEXPRESS;Initial Catalog=loan_db;Integrated Security=True;";

                sqlConnection = new SqlConnection(connectionString);
            }

            if (sqlConnection.State != ConnectionState.Open)
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

        public void CloseConnection()
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection OpenConnection()
        {
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }
                return sqlConnection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening SQL connection: {ex.Message}");
                return null;
            }
        }

        public string GetStringConnection()
        {
            try
            {
                if(this.connectionString == null && this.connectionString.ToString().Length <= 0)
                {
                    return "Data Source=DESKTOP-KOTS3GS\\SQLEXPRESS;Initial Catalog=loan_db;Integrated Security=True;";
                }

                return this.connectionString;
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Error opening SQL connection: {ex.Message}");
                return null;
            }
        }
    }
}

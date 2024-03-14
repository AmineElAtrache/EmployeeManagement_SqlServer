using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinalProjectDB
{
    class DBConnection
    {
        private string server = "DESKTOP-EG9BVJV"; // Replace with your SQL Server name or IP address
        private string database = "EmployeeManagement_FN";
        private string uid = "amine"; // Replace with your SQL Server username
        private string password = "0000"; // Replace with your SQL Server password

        public SqlConnection OpenConnection()
        {
            string connectionString = $"Data Source={server};Initial Catalog={database};User ID={uid};Password={password};";

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error:" + ex.Message);
                return null;
            }
        }
    }
}

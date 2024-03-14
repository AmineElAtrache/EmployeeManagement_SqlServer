using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectDB
{
    class TransactionType
    {
        private DBConnection conn = new DBConnection();

        public int Id { get; set; }
        public string TransactionTypeName { get; set; }

        public TransactionType()
        {
        }

        public TransactionType(string transactionName)
        {
            this.TransactionTypeName = transactionName;
        }

        public TransactionType(int id, string transactionName)
        {
            this.Id = id;
            this.TransactionTypeName = transactionName;
        }

        public void AddTransactionType()
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand insertTransactionType = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                insertTransactionType.CommandText = "INSERT INTO TransactionType(Name) VALUES (@transactionname)"; // Change table name to TransactionType
                insertTransactionType.Parameters.AddWithValue("@transactionname", TransactionTypeName);
                insertTransactionType.ExecuteNonQuery();
                MessageBox.Show("Data Inserted Successfully!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Show the exception message properly
            }
        }

        public List<TransactionType> RetrieveTransactionTypes()
        {
            try
            {
                List<TransactionType> transactionTypeList = new List<TransactionType>();
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand getTransactionTypes = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                getTransactionTypes.CommandText = "SELECT * FROM TransactionType"; // Change table name to TransactionType
                SqlDataReader transactionTypes = getTransactionTypes.ExecuteReader(); // Change MySqlDataReader to SqlDataReader
                while (transactionTypes.Read())
                {
                    int thisid = transactionTypes.GetInt32(0);
                    string thisname = transactionTypes.GetString(1);
                    transactionTypeList.Add(new TransactionType(Convert.ToInt32(thisid), thisname));
                }
                connection.Close();
                return transactionTypeList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Show the exception message properly
                return null;
            }
        }

        public void UpdateTransactionType()
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand updateTransactionType = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                updateTransactionType.CommandText = "UPDATE TransactionType SET Name = @name WHERE ID = @id"; // Change table name to TransactionType
                updateTransactionType.Parameters.AddWithValue("@name", TransactionTypeName);
                updateTransactionType.Parameters.AddWithValue("@id", Id);
                updateTransactionType.ExecuteNonQuery();
                MessageBox.Show("Data Updated Successfully!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Show the exception message properly
            }
        }

        public void DeleteTransactionType(int id)
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand deleteTransactionType = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                deleteTransactionType.CommandText = "DELETE FROM TransactionType WHERE ID = @id"; // Change table name to TransactionType
                deleteTransactionType.Parameters.AddWithValue("@id", id);
                deleteTransactionType.ExecuteNonQuery();
                MessageBox.Show("Data Deleted Successfully!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Show the exception message properly
            }
        }

    }
}

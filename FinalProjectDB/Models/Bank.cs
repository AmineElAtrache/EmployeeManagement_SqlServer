using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinalProjectDB
{
    class Bank
    {
        DBConnection conn = new DBConnection();

        public int Id { get; set; }
        public string BankName { get; set; }

        public Bank()
        {

        }
        public Bank(int id, string bankname)
        {
            this.Id = id;
            this.BankName = bankname;
        }
        public Bank(string bankName)
        {
            this.BankName = bankName;
        }

        public void AddBank()
        {
            try
            {
                SqlConnection connection = conn.OpenConnection();
                connection.Open();
                SqlCommand insertBank = connection.CreateCommand();
                insertBank.CommandText = "INSERT INTO Bank (Name) VALUES (@bankname)";
                insertBank.Parameters.AddWithValue("@bankname", BankName);
                insertBank.ExecuteNonQuery();
                MessageBox.Show("Data Inserted Successfully!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public List<Bank> RetrieveBanks()
        {
            try
            {
                List<Bank> bankList = new List<Bank>();
                SqlConnection connection = conn.OpenConnection();
                connection.Open();
                SqlCommand getBanks = connection.CreateCommand();
                getBanks.CommandText = "SELECT * FROM Bank";
                SqlDataReader banks = getBanks.ExecuteReader();
                while (banks.Read())
                {
                    int thisid = banks.GetInt32(0);
                    string thisname = banks.GetString(1);
                    bankList.Add(new Bank(Convert.ToInt32(thisid), thisname));
                }
                connection.Close();
                return bankList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public void UpdateBank()
        {
            try
            {
                SqlConnection connection = conn.OpenConnection();
                connection.Open();
                SqlCommand insertDepartment = connection.CreateCommand();
                insertDepartment.CommandText = "UPDATE Bank SET Name = @name WHERE ID = @id";
                insertDepartment.Parameters.AddWithValue("@name", BankName);
                insertDepartment.Parameters.AddWithValue("@id", Id);
                insertDepartment.ExecuteNonQuery();
                MessageBox.Show("Data Updated Successfully!");
                connection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void DeleteBank(int id)
        {
            try
            {
                SqlConnection connection = conn.OpenConnection();
                connection.Open();
                SqlCommand comd = connection.CreateCommand();
                comd.CommandText = "DELETE FROM Bank WHERE ID = @id";
                comd.Parameters.AddWithValue("@id", id);
                comd.ExecuteNonQuery();
                MessageBox.Show("Data Deleted Successfully!");
                connection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}

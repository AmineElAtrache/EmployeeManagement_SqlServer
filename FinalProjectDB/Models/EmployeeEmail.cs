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
    class EmployeeEmail
    {
        private DBConnection conn = new DBConnection();
        public int Id { get; set; }
        public int NIK { get; set; }
        public string emailAddress { get; set; }

        public EmployeeEmail()
        {
        }

        public EmployeeEmail(int nIK, string emailAddress)
        {
            NIK = nIK;
            this.emailAddress = emailAddress;
        }

        public EmployeeEmail(int id, int nIK, string emailAddress)
        {
            this.Id = id;
            NIK = nIK;
            this.emailAddress = emailAddress;
        }

        public void AddEmailAddress()
        {
            try
            {
                using (SqlConnection connection = conn.OpenConnection())
                {
                    connection.Open();
                    SqlCommand insertEmailAddress = connection.CreateCommand();
                    insertEmailAddress.CommandText = "INSERT INTO employee_email(NIK, EmailAddress) VALUES " +
                        "(@nik, @emailaddress)";
                    insertEmailAddress.Parameters.AddWithValue("@nik", NIK);
                    insertEmailAddress.Parameters.AddWithValue("@emailaddress", emailAddress);
                    insertEmailAddress.ExecuteNonQuery();
                    MessageBox.Show("Data Inserted Successfully!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }
        }

        public List<EmployeeEmail> RetrieveEmailAddress(int retNIK)
        {
            try
            {
                List<EmployeeEmail> emailAddressList = new List<EmployeeEmail>();
                using (SqlConnection connection = conn.OpenConnection())
                {
                    connection.Open();
                    SqlCommand getEmail = connection.CreateCommand();
                    getEmail.CommandText = "SELECT * FROM employee_email WHERE NIK = @nik";
                    getEmail.Parameters.AddWithValue("@nik", retNIK);
                    SqlDataReader emailAddresses = getEmail.ExecuteReader();
                    while (emailAddresses.Read())
                    {
                        int thisid = emailAddresses.GetInt32(emailAddresses.GetOrdinal("ID"));
                        int thisNIK = emailAddresses.GetInt32(emailAddresses.GetOrdinal("NIK"));
                        string thisEmailAddress = emailAddresses.GetString(emailAddresses.GetOrdinal("EmailAddress"));
                        emailAddressList.Add(new EmployeeEmail(Convert.ToInt32(thisid), thisNIK, thisEmailAddress));
                    }
                }
                return emailAddressList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
                return null;
            }
        }

        public void UpdateEmployeeEmail()
        {
            try
            {
                using (SqlConnection connection = conn.OpenConnection())
                {
                    connection.Open();
                    SqlCommand comd = connection.CreateCommand();
                    comd.CommandText = "UPDATE employee_email SET EmailAddress = @emailaddress WHERE NIK = @nik";
                    comd.Parameters.AddWithValue("@emailaddress", emailAddress);
                    comd.Parameters.AddWithValue("@nik", NIK);
                    comd.ExecuteNonQuery();
                    MessageBox.Show("Data Updated Successfully!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }
        }

        public void DeleteEmployeeEmail(int id)
        {
            try
            {
                using (SqlConnection connection = conn.OpenConnection())
                {
                    connection.Open();
                    SqlCommand comd = connection.CreateCommand();
                    comd.CommandText = "DELETE FROM employee_email WHERE ID = @id";
                    comd.Parameters.AddWithValue("@id", id);
                    comd.ExecuteNonQuery();
                    MessageBox.Show("Data Deleted Successfully!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }
        }

    }
}
﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectDB.Models
{
    class EmployeePhone
    {
        DBConnection conn = new DBConnection();
        public int Id { get; set; }
        public int NIK { get; set; }
        public string phoneNumber { get; set; }
        public string phoneType { get; set; }

        public EmployeePhone()
        {
        }

        public EmployeePhone(int nIK, string phoneNumber, string phoneType)
        {
            NIK = nIK;
            this.phoneNumber = phoneNumber;
            this.phoneType = phoneType;
        }

        public EmployeePhone(int id, int nIK, string phoneNumber, string phoneType)
        {
            Id = id;
            NIK = nIK;
            this.phoneNumber = phoneNumber;
            this.phoneType = phoneType;
        }

        public void AddEmployeePhone()
        {
            try
            {
                using (SqlConnection connection = conn.OpenConnection())
                {
                    connection.Open();
                    SqlCommand comd = connection.CreateCommand();
                    comd.CommandText = "INSERT INTO employee_phones(NIK, PhoneNumber, PhoneType) VALUES " +
                        "(@nik, @phonenumber, @phonetype)";
                    comd.Parameters.AddWithValue("@nik", NIK);
                    comd.Parameters.AddWithValue("@phonenumber", phoneNumber);
                    comd.Parameters.AddWithValue("@phonetype", phoneType);
                    comd.ExecuteNonQuery();
                    MessageBox.Show("Data Inserted Successfully!");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:", ex.ToString());
            }
        }

        public List<EmployeePhone> RetrievePhoneNumbers(int retNIK)
        {
            try
            {
                List<EmployeePhone> phoneNumbersList = new List<EmployeePhone>();
                using (SqlConnection connection = conn.OpenConnection())
                {
                    connection.Open();
                    SqlCommand comd = connection.CreateCommand();
                    comd.CommandText = "SELECT * FROM employee_phones WHERE NIK = @nik";
                    comd.Parameters.AddWithValue("@nik", retNIK);
                    SqlDataReader phoneNumbers = comd.ExecuteReader();
                    while (phoneNumbers.Read())
                    {
                        int thisid = phoneNumbers.GetInt32(0);
                        int thisNIK = phoneNumbers.GetInt32(1);
                        string thisphonenumber = phoneNumbers.GetString(2);
                        string thisphonetype = phoneNumbers.GetString(3);
                        phoneNumbersList.Add(new EmployeePhone(Convert.ToInt32(thisid), thisNIK, thisphonenumber, thisphonetype));
                    }
                }
                return phoneNumbersList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:", ex.ToString());
                return null;
            }
        }

        public void UpdateEmployeePhone()
        {
            try
            {
                using (SqlConnection connection = conn.OpenConnection())
                {
                    connection.Open();
                    SqlCommand comd = connection.CreateCommand();
                    comd.CommandText = "UPDATE employee_phones SET PhoneNumber = @phonenumber," +
                        " PhoneType = @phonetype" +
                        " WHERE NIK = @nik";
                    comd.Parameters.AddWithValue("@phonenumber", phoneNumber);
                    comd.Parameters.AddWithValue("@phonetype", phoneType);
                    comd.Parameters.AddWithValue("@nik", NIK);
                    comd.ExecuteNonQuery();
                    MessageBox.Show("Data Updated Successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:", ex.ToString());
            }
        }

        public void DeleteEmployeePhone(int id)
        {
            try
            {
                using (SqlConnection connection = conn.OpenConnection())
                {
                    connection.Open();
                    SqlCommand comd = connection.CreateCommand();
                    comd.CommandText = "DELETE FROM employee_phones WHERE ID = @id";
                    comd.Parameters.AddWithValue("@id", id);
                    comd.ExecuteNonQuery();
                    MessageBox.Show("Data Deleted Successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:", ex.ToString());
            }
        }

    }
}

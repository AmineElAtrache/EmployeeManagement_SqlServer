using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // Import SQL Server specific namespace
using System.Windows.Forms;

namespace FinalProjectDB
{
    class Department
    {
        private DBConnection conn = new DBConnection();

        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public Department()
        {
        }

        public Department(int id, string departmentName)
        {
            this.Id = id;
            this.DepartmentName = departmentName;
        }

        public void AddDepartment()
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand insertDepartment = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                insertDepartment.CommandText = "INSERT INTO Department(Name) VALUES (@departmentname)"; // Use "Department" instead of "department"
                insertDepartment.Parameters.AddWithValue("@departmentname", DepartmentName);
                insertDepartment.ExecuteNonQuery();
                MessageBox.Show("Data Inserted Successfully!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }
        }

        public void UpdateDepartment()
        {
            try
            {
                SqlConnection connection = conn.OpenConnection();
                connection.Open();
                SqlCommand insertDepartment = connection.CreateCommand();
                insertDepartment.CommandText = "UPDATE Department SET Name = @name WHERE ID = @id"; // Use "Department" instead of "department"
                insertDepartment.Parameters.AddWithValue("@name", DepartmentName);
                insertDepartment.Parameters.AddWithValue("@id", Id);
                insertDepartment.ExecuteNonQuery();
                MessageBox.Show("Data Updated Successfully!");
                connection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error:", ex.ToString());
            }
        }

        public void DeleteDepartment(int id)
        {
            try
            {
                SqlConnection connection = conn.OpenConnection();
                connection.Open();
                SqlCommand comd = connection.CreateCommand();
                comd.CommandText = "DELETE FROM Department WHERE ID = @id"; // Use "Department" instead of "department"
                comd.Parameters.AddWithValue("@id", id);
                comd.ExecuteNonQuery();
                MessageBox.Show("Data Deleted Successfully!");
                connection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error:", ex.ToString());
            }
        }

        public List<Department> RetrieveDepartments()
        {
            try
            {
                List<Department> departmentList = new List<Department>();
                SqlConnection connection = conn.OpenConnection();
                connection.Open();
                SqlCommand getDepartments = connection.CreateCommand();
                getDepartments.CommandText = "SELECT * FROM Department"; // Use "Department" instead of "department"
                SqlDataReader departments = getDepartments.ExecuteReader();
                while (departments.Read())
                {
                    int thisid = departments.GetInt32(0);
                    string thisname = departments.GetString(1);
                    departmentList.Add(new Department(Convert.ToInt32(thisid), thisname));
                }
                connection.Close();
                return departmentList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error:", ex.Message);
                return null;
            }
        }
    }
}

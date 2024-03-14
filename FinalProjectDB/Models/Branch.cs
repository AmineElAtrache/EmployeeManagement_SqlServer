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
    class Branch
    {
        private DBConnection conn = new DBConnection();
        public int Id { get; set; }
        public string BranchName { get; set; }

        public Branch()
        {
        }

        public Branch(string branchName)
        {
            this.BranchName = branchName;
        }

        public Branch(int id, string branchName)
        {
            this.Id = id;
            this.BranchName = branchName;
        }

        public void AddBranch()
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); 
                connection.Open();
                SqlCommand insertBranch = connection.CreateCommand();
                insertBranch.CommandText = "INSERT INTO [branch] ([Name]) VALUES (@branchname)"; 
                insertBranch.Parameters.AddWithValue("@branchname", BranchName);
                insertBranch.ExecuteNonQuery();
                MessageBox.Show("Data Inserted Successfully!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString()); // Concatenate exception message properly
            }
        }


        public List<Branch> RetrieveBranches()
        {
            try
            {
                List<Branch> branchList = new List<Branch>();
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand getBranches = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                getBranches.CommandText = "SELECT * FROM [branch]"; // Use square brackets for SQL Server
                SqlDataReader branches = getBranches.ExecuteReader(); // Change MySqlDataReader to SqlDataReader
                while (branches.Read())
                {
                    int thisid = branches.GetInt32(0);
                    string thisname = branches.GetString(1);
                    branchList.Add(new Branch(Convert.ToInt32(thisid), thisname));
                }
                connection.Close();
                return branchList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message); // Show the exception message properly
                return null;
            }
        }

        public void UpdateBranch()
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand updateBranch = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                updateBranch.CommandText = "UPDATE [branch] SET [Name] = @name WHERE [ID] = @id"; // Use square brackets for SQL Server
                updateBranch.Parameters.AddWithValue("@name", BranchName);
                updateBranch.Parameters.AddWithValue("@id", Id);
                updateBranch.ExecuteNonQuery();
                MessageBox.Show("Data Updated Successfully!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message); // Show the exception message properly
            }
        }


        public void DeleteBranch(int id)
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand deleteBranch = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                deleteBranch.CommandText = "DELETE FROM [branch] WHERE [ID] = @id"; // Use square brackets for SQL Server
                deleteBranch.Parameters.AddWithValue("@id", id);
                deleteBranch.ExecuteNonQuery();
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

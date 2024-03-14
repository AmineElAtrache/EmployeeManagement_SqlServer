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
    class Pos
    {
        private DBConnection conn = new DBConnection();
        public int Id { get; set; }
        public string PosName { get; set; }

        public Pos()
        {
        }

        public Pos(string posName)
        {
            this.PosName = posName;
        }

        public Pos(int id, string posName)
        {
            this.Id = id;
            this.PosName = posName;
        }

        public void AddPos()
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand insertPos = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                insertPos.CommandText = "INSERT INTO Pos(Name) VALUES (@posname)"; // Change table name to Pos
                insertPos.Parameters.AddWithValue("@posname", PosName);
                insertPos.ExecuteNonQuery();
                MessageBox.Show("Data Inserted Successfully!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Show the exception message properly
            }
        }

        public List<Pos> RetrievePos()
        {
            try
            {
                List<Pos> posList = new List<Pos>();
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand getPos = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                getPos.CommandText = "SELECT * FROM Pos"; // Change table name to Pos
                SqlDataReader poss = getPos.ExecuteReader(); // Change MySqlDataReader to SqlDataReader
                while (poss.Read())
                {
                    int thisid = poss.GetInt32(0);
                    string thisname = poss.GetString(1);
                    posList.Add(new Pos(Convert.ToInt32(thisid), thisname));
                }
                connection.Close();
                return posList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Show the exception message properly
                return null;
            }
        }

        public void UpdatePos()
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand updatePos = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                updatePos.CommandText = "UPDATE Pos SET Name = @name WHERE ID = @id"; // Change table name to Pos
                updatePos.Parameters.AddWithValue("@name", PosName);
                updatePos.Parameters.AddWithValue("@id", Id);
                updatePos.ExecuteNonQuery();
                MessageBox.Show("Data Updated Successfully!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Show the exception message properly
            }
        }


        public void DeletePos(int id)
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand comd = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                comd.CommandText = "DELETE FROM Pos WHERE ID = @id"; // Change table name to Pos
                comd.Parameters.AddWithValue("@id", id);
                comd.ExecuteNonQuery();
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

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
    class EmployeeRelationship
    {
        private DBConnection conn = new DBConnection();

        public int Id { get; set; }
        public string Relationship { get; set; }

        public EmployeeRelationship()
        {
        }

        public EmployeeRelationship(int id, string relationship)
        {
            this.Id = id;
            this.Relationship = relationship;
        }


        public List<EmployeeRelationship> RetrieveRelationships()
        {
            List<EmployeeRelationship> list = new List<EmployeeRelationship>();
            using (SqlConnection connection = conn.OpenConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM EmployeeRelationship";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int thisid = reader.GetInt32(reader.GetOrdinal("ID"));
                    string relationship = reader.GetString(reader.GetOrdinal("Relationship"));
                    list.Add(new EmployeeRelationship(Convert.ToInt32(thisid), relationship));
                }
            }
            return list;
        }

    }
}

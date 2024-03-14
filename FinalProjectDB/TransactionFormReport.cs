using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectDB
{
    public partial class TransactionFormReport : core
    {
        public TransactionFormReport()
        {
            InitializeComponent();
        }

        private void TransactionFormReport_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                DBConnection conn = new DBConnection();
                List<Department> list = new List<Department>();
                SqlConnection connection = conn.OpenConnection(); // Change MySqlConnection to SqlConnection
                connection.Open();
                SqlCommand cmd = connection.CreateCommand(); // Change MySqlCommand to SqlCommand
                cmd.CommandText = "select t.*, tt.Name 'TransactionType', ct.Name 'Contract Type', " +
                                    "b.name 'Branch', d.name 'Department', p.name 'Position', l.name 'Level' " +
                                    "from [transaction] t " +
                                    "join transactiontype tt " +
                                        "on t.transactiontypeid = tt.id " +
                                    "join contracttype ct " +
                                        "on t.contracttypeid = ct.id " +
                                    "join branch b " +
                                        "on t.branchid = b.id " +
                                    "join department d " +
                                        "on t.departmentid = d.id " +
                                    "join pos p " +
                                        "on t.positionid = p.id " +
                                    "join level l " +
                                        "on t.levelid = l.id";
                //MySqlDataReader reader = cmd.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(cmd); // Change MySqlDataAdapter to SqlDataAdapter
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }
        }
    }
}

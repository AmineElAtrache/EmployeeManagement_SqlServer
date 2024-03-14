using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // Import SQL Server specific namespace
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectDB
{
    public partial class EditTransactionForm : core
    {
        bool formReady = false;

        public EditTransactionForm()
        {
            InitializeComponent();
        }

        private void EditTransactionForm_Load(object sender, EventArgs e)
        {
            var employee = new Employee();
            var q = employee.RetrieveEmployees().Select(x => new
            {
                Display = x.nickname + " - " + x.NIK,
                Value = x
            }).ToList();
            comboBox1.DisplayMember = "Display";
            comboBox1.ValueMember = "Value";
            comboBox1.DataSource = q;

            formReady = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formReady)
            {
                LoadEmployeeData();
            }
        }

        private void LoadEmployeeData()
        {
            var employee = (Employee)comboBox1.SelectedValue;

            var conn = new DBConnection();
            List<Department> list = new List<Department>();

            // Change MySqlConnection to SqlConnection
            SqlConnection connection = conn.OpenConnection();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            // Adjust SQL query for SQL Server syntax
            cmd.CommandText = "SELECT * FROM [transaction] WHERE NIK = @NIK";
            cmd.Parameters.AddWithValue("@NIK", employee.NIK);

            // Use SqlDataAdapter to fill DataTable
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

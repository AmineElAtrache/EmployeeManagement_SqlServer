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
    public partial class LoginForm : core
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            button2.BackColor = Color.PaleVioletRed;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Validation(container))
            {
                MessageBox.Show("All data must be filled");
                return;
            }

            // Define connection string with SQL Server authentication
            string connectionString = "Data Source=DESKTOP-EG9BVJV;Initial Catalog=EmployeeManagement_FN;User ID=amine;Password=0000;";

            // Create SQL connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open connection
                    connection.Open();

                    // Define SQL query to retrieve data from the Users table
                    string query = "SELECT * FROM [user]";

                    // Create SqlDataAdapter
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    // Create DataSet
                    DataSet dataset = new DataSet();

                    // Fill DataSet with data from the Users table
                    adapter.Fill(dataset, "user");

                    // Check if user exists in the dataset
                    string username = textBox1.Text;
                    string password = textBox2.Text;

                    DataRow[] foundRows = dataset.Tables["user"].Select($"username = '{username}' AND password = '{password}'");
                    if (foundRows.Length == 0)
                    {
                        MessageBox.Show("Invalid username or password");
                        return;
                    }

                    // If you want to use the dataset for further processing, you can assign it to a global variable
                    // For example:
                    // GlobalDataset = dataset;

                    // Close connection
                    connection.Close();

                    // Assuming validation passed and user exists, navigate directly to the main form
                    formLogin = this;

                    // Close the current form and open the MainForm
                    this.Hide();
                    new MainForm().Show();

                    // Clear text boxes if needed
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting to database: " + ex.Message);
                }
            }
        }


    }
}
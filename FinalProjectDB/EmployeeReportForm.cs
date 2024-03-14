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
using System.Windows.Forms.DataVisualization.Charting;

namespace FinalProjectDB
{
    public partial class EmployeeReportForm : core
    {
        public EmployeeReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            LoadGenderChart();
            LoadMaritalStatus();
            LoadAges();
        }
        private DBConnection conn = new DBConnection();

        public void LoadGenderChart()
        {
            chart1.Titles.Clear();
            chart1.Titles.Add("");
            chart1.Titles[0].Text = "Gender Percentage";
            chart1.Titles[0].Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

            chart1.Series.Clear();
            Series s = chart1.Series.Add("");
            s.XValueMember = "Gender";
            s.YValueMembers = "Total";
            s.ChartType = SeriesChartType.Pie;
            s.IsValueShownAsLabel = true;

            try
            {
                using (SqlConnection connection = conn.OpenConnection())
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT Gender, COUNT(*) AS Total FROM Employee GROUP BY Gender";
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Gender");
                    dt.Columns.Add("Total");
                    while (reader.Read())
                    {
                        var gender = reader.GetString(0);
                        dt.Rows.Add(gender == "M" ? "Male" : gender == "L" ? "Male" : "Female", reader.GetInt32(1));
                    }
                    connection.Close();

                    chart1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void LoadMaritalStatus()
        {
            chart2.Titles.Clear();
            chart2.Titles.Add("");
            chart2.Titles[0].Text = "Marital Status";
            chart2.Titles[0].Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

            chart2.Series.Clear();
            Series s = chart2.Series.Add("Marital Status");
            s.XValueMember = "MaritalStatus";
            s.YValueMembers = "Total";
            s.ChartType = SeriesChartType.Column;
            s.IsValueShownAsLabel = true;

            try
            {
                using (SqlConnection connection = conn.OpenConnection())
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MaritalStatus, COUNT(*) AS Total FROM Employee GROUP BY MaritalStatus";
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("MaritalStatus");
                    dt.Columns.Add("Total");
                    while (reader.Read())
                    {
                        var maritalStatus = reader.GetString(0);
                        dt.Rows.Add(maritalStatus, reader.GetInt32(1));
                    }
                    connection.Close();

                    chart2.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void LoadAges()
        {
            chart3.Titles.Clear();
            chart3.Titles.Add("");
            chart3.Titles[0].Text = "Age";
            chart3.Titles[0].Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

            chart3.Series.Clear();
            Series s = chart3.Series.Add("");
            s.XValueMember = "AgeCategory";
            s.YValueMembers = "Total";
            s.ChartType = SeriesChartType.Column;
            s.IsValueShownAsLabel = true;

            try
            {
                using (SqlConnection connection = conn.OpenConnection())
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT CASE " +
                                                "WHEN YEAR(GETDATE()) - YEAR(DOB) < 20 THEN 'Under 20' " +
                                                "WHEN YEAR(GETDATE()) - YEAR(DOB) <= 30 THEN '20 - 30' " +
                                                "WHEN YEAR(GETDATE()) - YEAR(DOB) <= 40 THEN '31 - 40' " +
                                                "ELSE 'Above 40' " +
                                            "END AS AgeCategory, " +
                                            "COUNT(*) AS Total " +
                                        "FROM Employee GROUP BY CASE " +
                                                "WHEN YEAR(GETDATE()) - YEAR(DOB) < 20 THEN 'Under 20' " +
                                                "WHEN YEAR(GETDATE()) - YEAR(DOB) <= 30 THEN '20 - 30' " +
                                                "WHEN YEAR(GETDATE()) - YEAR(DOB) <= 40 THEN '31 - 40' " +
                                                "ELSE 'Above 40' " +
                                            "END";
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("AgeCategory");
                    dt.Columns.Add("Total");
                    while (reader.Read())
                    {
                        var ageCategory = reader.GetString(0);
                        dt.Rows.Add(ageCategory, reader.GetInt32(1));
                    }
                    connection.Close();

                    chart3.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void container_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

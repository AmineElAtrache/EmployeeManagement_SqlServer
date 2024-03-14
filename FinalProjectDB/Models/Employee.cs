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
    class Employee
    {
        private DBConnection conn = new DBConnection();
        public int NIK { get; set; }
        public string fullName { get; set; }
        public string nickname { get; set; }
        public string KTP { get; set; }
        public string jamsostek { get; set; }
        public int bankID { get; set; }
        public string BankName { get; set; }
        public string rekening { get; set; }
        public string NPWP { get; set; }
        public string statusPajak { get; set; }
        public DateTime DOB { get; set; }
        public string gender { get; set; }
        public string religion { get; set; }
        public string maritalStatus { get; set; }


        public Employee()
        {

        }

        public Employee(int nIK, string fullName, string nickname, string kTP, string jamsostek, int bankID, string rekening, string nPWP, string statusPajak, DateTime dOB, string gender, string religion, string maritalStatus)
        {
            this.NIK = nIK;
            this.fullName = fullName;
            this.nickname = nickname;
            this.KTP = kTP;
            this.jamsostek = jamsostek;
            this.bankID = bankID;
            this.rekening = rekening;
            this.NPWP = nPWP;
            this.statusPajak = statusPajak;
            this.DOB = dOB;
            this.gender = gender;
            this.religion = religion;
            this.maritalStatus = maritalStatus;
        }

        public void CreateEmployee()
        {
            SqlConnection connection = conn.OpenConnection(); // Assuming conn.OpenConnection() returns SqlConnection
            connection.Open();

            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Employee(NIK, FullName, Nickname, KTP, " +
                "Jamsostek, BankID, Rekening, NPWP, StatusPajak, DOB, Gender, Religion, MaritalStatus) " +
                "VALUES (@nik, @fullname, @nickname, @ktp, @jamsostek, @bankid, @rekening, @npwp, " +
                "@statuspajak, @dob, @gender, @religion, @maritalstatus)";

            cmd.Parameters.AddWithValue("@nik", NIK.ToString());
            cmd.Parameters.AddWithValue("@fullname", fullName);
            cmd.Parameters.AddWithValue("@nickname", nickname);
            cmd.Parameters.AddWithValue("@ktp", KTP);
            cmd.Parameters.AddWithValue("@jamsostek", jamsostek);
            cmd.Parameters.AddWithValue("@bankid", bankID.ToString());
            cmd.Parameters.AddWithValue("@rekening", rekening);
            cmd.Parameters.AddWithValue("@npwp", NPWP);
            cmd.Parameters.AddWithValue("@statuspajak", statusPajak);
            cmd.Parameters.AddWithValue("@dob", DOB);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@religion", religion);
            cmd.Parameters.AddWithValue("@maritalstatus", maritalStatus);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Created Successfully!");
            connection.Close();
        }

        public List<Employee> RetrieveEmployees()
        {
            List<Employee> list = new List<Employee>();
            SqlConnection connection = conn.OpenConnection();
            connection.Open();
            SqlCommand getEmployees = connection.CreateCommand();
            getEmployees.CommandText = "SELECT e.NIK, e.FullName, e.Nickname, e.KTP, e.Jamsostek, e.BankID, e.Rekening, e.NPWP, e.StatusPajak, e.DOB, e.Gender, e.Religion, e.MaritalStatus, b.Name " +
                                        "FROM Employee e " +
                                        "JOIN Bank b ON e.BankID = b.ID"; // Adjusted the query to SQL Server syntax
            SqlDataReader reader = getEmployees.ExecuteReader();

            while (reader.Read())
            {
                int nik = reader.GetInt32(0); // Adjusted to use GetInt32
                var fullName = reader.GetString(1);
                var nickName = reader.GetString(2);
                var ktp = reader.GetString(3);
                var jamsostek = reader.GetString(4);
                var bankId = reader.GetInt32(5); // Adjusted to use GetInt32
                var rekening = reader.GetString(6);
                var npwp = reader.GetString(7);
                var statusPajak = reader.GetString(8);
                var dob = reader.GetDateTime(9);
                var gender = reader.GetString(10);
                var religion = reader.GetString(11);
                var maritalStatus = reader.GetString(12);
                var bankName = reader.GetString(13); // Adjusted to read bank name from the correct column index

                var employee = new Employee(nik, fullName, nickName, ktp, jamsostek, bankId, rekening, npwp, statusPajak, dob, gender, religion, maritalStatus);
                employee.BankName = bankName;
                list.Add(employee);
            }
            connection.Close();
            return list;
        }

        public void UpdateEmployee()
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); // Assuming conn.OpenConnection() returns SqlConnection
                connection.Open();
                SqlCommand comd = connection.CreateCommand();
                comd.CommandText = "UPDATE Employee SET Fullname = @fullname, Nickname = @nickname, " +
                    "KTP = @ktp, Jamsostek = @jamsostek, " +
                    "BankID = @bankid, Rekening = @rekening, " +
                    "NPWP = @npwp, StatusPajak = @statuspajak, " +
                    "DOB = @dob, Gender = @gender, " +
                    "Religion = @religion, MaritalStatus = @maritalstatus " +
                    "WHERE NIK = @nik";
                comd.Parameters.AddWithValue("@fullname", fullName);
                comd.Parameters.AddWithValue("@nickname", nickname);
                comd.Parameters.AddWithValue("@ktp", KTP);
                comd.Parameters.AddWithValue("@jamsostek", jamsostek);
                comd.Parameters.AddWithValue("@bankid", bankID);
                comd.Parameters.AddWithValue("@rekening", rekening);
                comd.Parameters.AddWithValue("@npwp", NPWP);
                comd.Parameters.AddWithValue("@statuspajak", statusPajak);
                comd.Parameters.AddWithValue("@dob", DOB);
                comd.Parameters.AddWithValue("@gender", gender);
                comd.Parameters.AddWithValue("@religion", religion);
                comd.Parameters.AddWithValue("@maritalstatus", maritalStatus);
                comd.Parameters.AddWithValue("@NIK", NIK);
                comd.ExecuteNonQuery();
                MessageBox.Show("Data Updated Successfully!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:", ex.Message);
            }
        }

        public void DeleteEmployee(int delNIK)
        {
            try
            {
                SqlConnection connection = conn.OpenConnection(); // Assuming conn.OpenConnection() returns SqlConnection
                connection.Open();
                SqlCommand comd = connection.CreateCommand();
                comd.CommandText = "DELETE FROM Employee WHERE NIK = @nik";
                comd.Parameters.AddWithValue("@nik", delNIK);
                comd.ExecuteNonQuery();
                MessageBox.Show("Data Deleted Successfully!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:", ex.Message);
            }
        }

    }
}
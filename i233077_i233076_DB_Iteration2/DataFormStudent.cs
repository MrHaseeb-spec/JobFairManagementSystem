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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project
{
    public partial class DataFormStudent : Form
    {
        public DataFormStudent()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            string status = statusComboBox.SelectedItem?.ToString();
            string batchYearText = batchYearTextBox.Text.Trim();

            if (string.IsNullOrEmpty(status) || string.IsNullOrEmpty(batchYearText))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!int.TryParse(batchYearText, out int batchYear) || batchYear < 2000 || batchYear > DateTime.Now.Year)
            {
                MessageBox.Show("Invalid batch year.");
                return;
            }

            //Convert email into studentId
            string studentId = "";
            studentId += '2';
            studentId += UserData.SelectedEmail[2];
            studentId += 'I';
            studentId += '-';
            for (int i=3;i<7;i++)
            {
                studentId += UserData.SelectedEmail[i];
            }

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string insertQuery = "INSERT INTO STUDENT (studentId, batchYear, status, userId) VALUES (@studentId, @batchYear, @status, @userId)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, conn);

            insertCmd.Parameters.AddWithValue("@studentId", studentId);
            insertCmd.Parameters.AddWithValue("@batchYear", batchYear);
            insertCmd.Parameters.AddWithValue("@status", status);
            insertCmd.Parameters.AddWithValue("@userId", UserData.SelectedUserId);
            insertCmd.ExecuteNonQuery();
            StudentData.SelectedStudentStatus = status;
            StudentData.SelectedStudentID = studentId;
            StudentData.SelectedBatchYear = batchYear;
            MessageBox.Show("Data submitted successfully!");
            this.Hide();
            StudentDashBoard loginForm = new StudentDashBoard();
            loginForm.Show();
        }
    }
}

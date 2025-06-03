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

namespace Project
{
    public partial class DataFormRecruiter : Form
    {
        public DataFormRecruiter()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            string designationText = DesignationTextBox.Text.Trim();

            if (string.IsNullOrEmpty(designationText))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string insertQuery = "INSERT INTO RECRUITER (designation, userId) VALUES (@designation, @userId)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, conn);

            insertCmd.Parameters.AddWithValue("@designation", designationText);
            insertCmd.Parameters.AddWithValue("@userId", UserData.SelectedUserId);
            insertCmd.ExecuteNonQuery();
            RecruiterData.SelectedRecruiterDesignation = designationText;
            MessageBox.Show("Data submitted successfully!");
            this.Hide();
            RecruiterDashBoard loginForm = new RecruiterDashBoard();
            loginForm.Show();
        }
    }
}

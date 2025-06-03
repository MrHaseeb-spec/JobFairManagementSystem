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
    public partial class ReviewApplication : Form
    {
        public ReviewApplication()
        {
            InitializeComponent();
            LoadApplications();
        }
      

        private void LoadApplications()
        {
            int recruiterId = RecruiterData.SelectedRecruiterID;

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT applicationId, applicationDate, status, jobPostingId, studentId
            FROM APPLICATION
            WHERE recruiterId = @recruiterId AND status = 'Pending'";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@recruiterId", recruiterId);

                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;
            }
        }
        

        

        private void UpdateApplicationStatus(string newStatus)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an application.");
                return;
            }

            int applicationId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["applicationId"].Value);

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE APPLICATION SET status = @status WHERE applicationId = @applicationId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@applicationId", applicationId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show($"Application {applicationId} marked as {newStatus}.");
            LoadApplications(); // Refresh grid
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UpdateApplicationStatus("Accepted");
            LoadApplications();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UpdateApplicationStatus("Rejected");
            LoadApplications();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            RecruiterDashBoard dashboard = new RecruiterDashBoard();
            dashboard.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReviewApplication dashboard = new ReviewApplication();
            dashboard.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (RecruiterData.SelectedCompanyID == 0)
            {
                this.Hide();
                CompanyDataForm dashboard = new CompanyDataForm();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Recruiter already has a company");
                this.Hide();
                RecruiterDashBoard dashboard = new RecruiterDashBoard();
                dashboard.Show();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            PostJob job = new PostJob();
            job.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            ScheduleInterview scheduleInterview = new ScheduleInterview();
            scheduleInterview.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hiring hiring = new Hiring();
            hiring.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}

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
    public partial class Hiring : Form
    {
        public Hiring()
        {
            InitializeComponent();
            LoadCompletedInterviews(RecruiterData.SelectedRecruiterID);
        }
        private void LoadCompletedInterviews(int selectedRecruiterId)
        {
            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";
            string query = @"SELECT interviewId, interviewDate, interviewTime, interviewStatus 
                 FROM INTERVIEW 
                 WHERE interviewStatus = 'Completed' 
                   AND recruiterId = @RecruiterId
                   AND interviewId NOT IN (SELECT interviewId FROM HIRING_OUTCOME)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RecruiterId", selectedRecruiterId);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = table;

                        // Optional: Auto-size columns
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading completed interviews: " + ex.Message);
                    }
                }
            }
        }
        private void InsertHiringOutcome(string status)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an interview.");
                return;
            }

            if (status == "Hired" && string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter a package for hired candidate.");
                return;
            }

            int interviewId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["interviewId"].Value);
            int? packageOffered = null;

            if (status == "Hired")
            {
                if (int.TryParse(textBox2.Text, out int package))
                {
                    packageOffered = package;
                }
                else
                {
                    MessageBox.Show("Invalid package value.");
                    return;
                }
            }

            string remarks = textBox1.Text.Trim();
            DateTime today = DateTime.Today;

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";
            string query = @"INSERT INTO HIRING_OUTCOME (finalStatus, decisionDate, packageOffered, remarks, interviewId)
                     VALUES (@Status, @DecisionDate, @PackageOffered, @Remarks, @InterviewId)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@DecisionDate", today);
                cmd.Parameters.AddWithValue("@Remarks", remarks);
                cmd.Parameters.AddWithValue("@InterviewId", interviewId);

                if (packageOffered.HasValue)
                    cmd.Parameters.AddWithValue("@PackageOffered", packageOffered.Value);
                else
                    cmd.Parameters.AddWithValue("@PackageOffered", DBNull.Value);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hiring outcome recorded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting hiring outcome: " + ex.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertHiringOutcome("Hired");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InsertHiringOutcome("Rejected");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            RecruiterDashBoard dashboard = new RecruiterDashBoard();
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
            PostJob dashboard = new PostJob();
            dashboard.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReviewApplication dashboard = new ReviewApplication();
            dashboard.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            ScheduleInterview dashboard = new ScheduleInterview();
            dashboard.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hiring dashboard = new Hiring();
            dashboard.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}

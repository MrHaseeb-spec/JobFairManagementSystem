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
    public partial class JobViewstd : Form
    {
        public JobViewstd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentDashBoard dashboard = new StudentDashBoard();
            dashboard.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            JobViewstd JobList = new JobViewstd();
            JobList.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            ScheduleInterviewStd interview = new ScheduleInterviewStd();
            interview.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewJobFairEvents events = new ViewJobFairEvents();
            events.Show();
        }
        private void SearchAndFilter()
        {


        }
        public List<Jobs> GetJobs(string filter, string search)
        {
            var jobs = new List<Jobs>();
            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "";
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                if (filter == "Job Type")
                {
                    query = "SELECT * FROM JOBPOSTING WHERE JobType LIKE @search";
                    command.Parameters.AddWithValue("@search", "%" + search + "%");
                }
                else if (filter == "Required Skills")
                {
                    query = "SELECT * FROM JOBPOSTING WHERE RequiredSkills LIKE @search";
                    command.Parameters.AddWithValue("@search", "%" + search + "%");
                }
                else if (filter == "Salary Range")
                {
                    string[] range = search.Split('-');
                    if (range.Length == 2 &&
                        int.TryParse(range[0], out int minSalary) &&
                        int.TryParse(range[1], out int maxSalary))
                    {
                        query = "SELECT * FROM JOBPOSTING WHERE Salary BETWEEN @min AND @max";
                        command.Parameters.AddWithValue("@min", minSalary);
                        command.Parameters.AddWithValue("@max", maxSalary);
                    }
                    else
                    {
                        MessageBox.Show("Please enter salary range in format: min-max");
                        return jobs; // return empty if invalid range
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid filter.");
                    return jobs;
                }

                command.CommandText = query;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Jobs job = new Jobs
                        {
                            SelectedJobId = reader.GetInt32(reader.GetOrdinal("JobPostingId")),
                            SelectedJobTitle = reader.GetString(reader.GetOrdinal("title")),
                            SelectedJobType = reader.GetString(reader.GetOrdinal("JobType")),
                            SelectedJobStartingSalary = reader.GetDecimal(reader.GetOrdinal("StartingSalary")),
                            SelectedJobEndingSalary = reader.GetDecimal(reader.GetOrdinal("EndingSalary")),
                            SelectedJobDescription = reader.GetString(reader.GetOrdinal("Description")),
                            SelectedjobStatus = reader.GetString(reader.GetOrdinal("Status")),
                            SelectedjobRecruiterId = reader.GetInt32(reader.GetOrdinal("recruiterId")),
                            

                            // Add other properties if available in your table
                        };

                        jobs.Add(job);
                    }
                }
            }

            return jobs;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int jobPostingId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SelectedJobId"].Value);
                int recruiterId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SelectedjobRecruiterId"].Value);
                string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Prevent duplicate application using try-catch or checking manually
                    string insertQuery = @"
                INSERT INTO APPLICATION (applicationDate, status, jobPostingId, studentId, recruiterId)
                VALUES (@date, @status, @jobId, @studentId, @recruiterId)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@status", "Pending");
                        cmd.Parameters.AddWithValue("@jobId", jobPostingId);
                        cmd.Parameters.AddWithValue("@studentId", StudentData.SelectedStudentID);
                        cmd.Parameters.AddWithValue("@recruiterId", recruiterId);

                        try
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Application submitted successfully!");
                            }
                            else
                            {
                                MessageBox.Show("Failed to apply.");
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 2627) // Unique constraint violation
                            {
                                MessageBox.Show("You have already applied to this job.");
                            }
                            else
                            {
                                MessageBox.Show("An error occurred: " + ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a job from the list to apply.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filter = comboBox1.Text;
            string search = textBox1.Text;

            var jobResults = GetJobs(filter, search);
            dataGridView1.DataSource = jobResults;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                RequiredJobSkillsShow.jobId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SelectedJobId"].Value);
                this.Hide();
                RequiredSkillsDetail form = new RequiredSkillsDetail();
                form.Show();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Project
{
    public partial class PostJob : Form
    {
        public PostJob()
        {
            InitializeComponent();

            // Add Description column if not already present
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn descColumn = new DataGridViewTextBoxColumn
                {
                    Name = "description",
                    HeaderText = "Description",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                dataGridView1.Columns.Add(descColumn);
            }

            showAlreadyEnteredDetails();
            LoadRequiredSkillsIntoGrid();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            LoadRequiredSkillsIntoGrid();
        }

        void showAlreadyEnteredDetails()
        {
            // Set UI fields from PostjobDetails regardless of whether they're empty
            textBox1.Text = PostjobDetails.title;
            textBox2.Text = PostjobDetails.startingSalary;
            textBox3.Text = PostjobDetails.description;
            textBox5.Text = PostjobDetails.endingSalary;
            comboBox1.Text = PostjobDetails.jobType;
            comboBox2.Text = PostjobDetails.status;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Always update the PostjobDetails with latest input
            PostjobDetails.title = textBox1.Text.Trim();
            PostjobDetails.description = textBox3.Text.Trim();
            PostjobDetails.jobType = comboBox1.Text.Trim();
            PostjobDetails.status = comboBox2.Text.Trim();
            PostjobDetails.startingSalary = textBox2.Text.Trim();
            PostjobDetails.endingSalary = textBox5.Text.Trim();

            if (!decimal.TryParse(PostjobDetails.startingSalary, out decimal startingSalary) ||
                !decimal.TryParse(PostjobDetails.endingSalary, out decimal endingSalary) ||
                endingSalary < startingSalary)
            {
                MessageBox.Show("Please enter valid salary values.");
                return;
            }

            List<string> requiredSkills = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    requiredSkills.Add(row.Cells[0].Value.ToString().Trim());
                }
            }

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string insertJobQuery = @"INSERT INTO JOBPOSTING 
                        (title, jobType, startingSalary, endingSalary, description, status, recruiterId)
                        VALUES (@title, @jobType, @startingSalary, @endingSalary, @description, @status, @recruiterId);
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand jobCmd = new SqlCommand(insertJobQuery, conn, transaction);
                    jobCmd.Parameters.AddWithValue("@title", PostjobDetails.title);
                    jobCmd.Parameters.AddWithValue("@jobType", PostjobDetails.jobType);
                    jobCmd.Parameters.AddWithValue("@startingSalary", PostjobDetails.startingSalary);
                    jobCmd.Parameters.AddWithValue("@endingSalary", PostjobDetails.endingSalary);
                    jobCmd.Parameters.AddWithValue("@description", PostjobDetails.description);
                    jobCmd.Parameters.AddWithValue("@status", PostjobDetails.status);
                    jobCmd.Parameters.AddWithValue("@recruiterId", RecruiterData.SelectedRecruiterID);

                    int jobPostingId = Convert.ToInt32(jobCmd.ExecuteScalar());

                    foreach (var skill in requiredSkills)
                    {
                        string insertSkillQuery = @"INSERT INTO REQUIREDSKILLS (description, jobPostingId)
                                                    VALUES (@desc, @jobId)";
                        SqlCommand skillCmd = new SqlCommand(insertSkillQuery, conn, transaction);
                        skillCmd.Parameters.AddWithValue("@desc", skill);
                        skillCmd.Parameters.AddWithValue("@jobId", jobPostingId);
                        skillCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Job posted successfully!");
                    this.Hide();
                    new RecruiterDashBoard().Show();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new AddRequiredSkills().Show(); // Just open skill form
        }

        private void LoadRequiredSkillsIntoGrid()
        {
            dataGridView1.Rows.Clear();

            foreach (string skill in RequiredSkills.SkillList)
            {
                dataGridView1.Rows.Add(skill);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new RecruiterDashBoard().Show();
        }
    }
}

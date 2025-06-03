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
    public partial class ReviewStd : Form
    {
        public ReviewStd()
        {
            InitializeComponent();
            LoadInterviewsForReview();
        }
        public List<Interview> GetCompletedInterviewsForReview()
        {
            List<Interview> interviews = new List<Interview>();

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            SELECT I.interviewId, I.interviewDate, I.interviewTime, I.interviewStatus, I.applicationId, I.recruiterId, I.boothId
            FROM INTERVIEW I
            INNER JOIN APPLICATION A ON I.applicationId = A.applicationId
            WHERE A.studentId = @studentId AND I.interviewStatus = 'Completed' AND I.interviewId NOT IN (SELECT interviewId FROM Review)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@studentId", StudentData.SelectedStudentID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Interview interview = new Interview
                            {
                                SelectedInterviewId = reader.GetInt32(reader.GetOrdinal("interviewId")),
                                SelectedInterviewDate = reader.GetDateTime(reader.GetOrdinal("interviewDate")).ToString(),
                                SelectedInterviewTime = reader.GetTimeSpan(reader.GetOrdinal("interviewTime")).ToString(),
                                SelectedInterviewStatus = reader.GetString(reader.GetOrdinal("interviewStatus")),
                                SelectedApplicationId = reader.GetInt32(reader.GetOrdinal("applicationId")),
                                SelectedRecruiterId = reader.GetInt32(reader.GetOrdinal("recruiterId")),
                                SelectedBoothId = reader.GetInt32(reader.GetOrdinal("boothId"))
                            };

                            interviews.Add(interview);
                        }
                    }
                }
            }

            return interviews;
        }
        private void LoadInterviewsForReview()
        {
            List<Interview> interviews = GetCompletedInterviewsForReview();
            dataGridView2.DataSource = interviews;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewJobFairEvents events = new ViewJobFairEvents();
            events.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReviewStd review = new ReviewStd();
            review.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            this.Hide();
            ScheduleInterviewStd interview = new ScheduleInterviewStd();
            interview.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an interview.");
                return;
            }

            string comments = textBox1.Text.Trim();
            string ratingText = textBox6.Text.Trim();

            if (!decimal.TryParse(ratingText, out decimal rating) || rating < 0 || rating > 5)
            {
                MessageBox.Show("Please enter a valid rating between 0 and 5.");
                return;
            }

            int interviewId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["SelectedInterviewId"].Value);
            string studentId = StudentData.SelectedStudentID;

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO REVIEW (comments, rating, studentId, interviewId)
                         VALUES (@comments, @rating, @studentId, @interviewId)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@comments", comments);
                    cmd.Parameters.AddWithValue("@rating", rating);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    cmd.Parameters.AddWithValue("@interviewId", interviewId);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Review submitted successfully!");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error submitting review: " + ex.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            JobViewstd JobList = new JobViewstd();
            JobList.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentDashBoard dashboard = new StudentDashBoard();
            dashboard.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}

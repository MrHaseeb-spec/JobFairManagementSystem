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
    public partial class ScheduleInterviewStd : Form
    {
        public ScheduleInterviewStd()
        {
            InitializeComponent();
            LoadAvailableScheduledInterviewsForStudent();
        }
        private void LoadAvailableScheduledInterviewsForStudent()
        {
            string query = @"SELECT I.interviewId, I.interviewDate, I.interviewTime, I.interviewStatus
                             FROM INTERVIEW I
                             JOIN RECRUITER R ON I.recruiterId = R.recruiterId
                             WHERE I.applicationId IS NULL
                             AND I.interviewStatus = 'Scheduled'
                             AND R.recruiterId IN (
                             SELECT A.recruiterId
                             FROM APPLICATION A
                             WHERE A.studentId = @StudentId);";

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", StudentData.SelectedStudentID);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dataGridView1.DataSource = table;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading interviews: " + ex.Message);
                    }
                }
            }
        }
        /*
         public List<Interview> GetAvailableInterviews()
         {
             var interviews = new List<Interview>();

             using (SqlConnection conn = new SqlConnection("Data source = ALMIGHTYPUSH\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
             {
                 conn.Open();
                 string query = @"SELECT I.interviewId, I.interviewDate, I.interviewTime, I.interviewStatus
                                  FROM INTERVIEW I
                                  JOIN RECRUITER R ON I.recruiterId = R.recruiterId
                                  WHERE I.applicationId IS NULL
                                  AND I.interviewStatus = 'Scheduled'
                                  AND R.recruiterId IN (
                                  SELECT A.recruiterId
                                  FROM APPLICATION A
                                  WHERE A.studentId = @StudentId)";

                 SqlCommand cmd = new SqlCommand(query, conn);

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
                             SelectedApplicationId = reader.GetInt32(reader.GetOrdinal("applicationhId")),
                             SelectedRecruiterId = reader.GetInt32(reader.GetOrdinal("recruiterId")),
                             SelectedBoothId = reader.GetInt32(reader.GetOrdinal("boothId")),

                         };

                         interviews.Add(interview);
                     }
                 }
             }

             return interviews;
         }
         private void LoadInterviewData()
         {
             List<Interview> interviewList = GetAvailableInterviews();
             dataGridView1.DataSource = interviewList;
         }
        */

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an interview.");
                return;
            }

            int selectedInterviewId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["interviewId"].Value);

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Step 1: Get applicationId
                int applicationId = -1;
                using (SqlCommand getAppCmd = new SqlCommand(@"SELECT applicationId
                                                               FROM APPLICATION
                                                               WHERE studentId = @StudentId
                                                               AND recruiterId = (
                                                               SELECT recruiterId FROM INTERVIEW WHERE interviewId = @InterviewId)", conn))
                {
                    getAppCmd.Parameters.AddWithValue("@StudentId", StudentData.SelectedStudentID);
                    getAppCmd.Parameters.AddWithValue("@InterviewId", selectedInterviewId);

                    object result = getAppCmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("No matching application found for this student and interview.");
                        return;
                    }

                    applicationId = Convert.ToInt32(result);
                }

                // Step 2: Update interview
                using (SqlCommand updateCmd = new SqlCommand(@"BEGIN TRANSACTION;
                                                               UPDATE INTERVIEW
                                                               SET applicationId = @ApplicationId,
                                                               interviewStatus = 'Completed'
                                                               WHERE interviewId = @InterviewId;
                                                                
                                                               UPDATE BOOTH
                                                               SET studentVisited = studentVisited + 1
                                                               WHERE boothId = (
                                                               SELECT boothId FROM INTERVIEW WHERE interviewId = @InterviewId);
                                                               COMMIT TRANSACTION;", conn))
                {
                    updateCmd.Parameters.AddWithValue("@ApplicationId", applicationId);
                    updateCmd.Parameters.AddWithValue("@InterviewId", selectedInterviewId);

                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        MessageBox.Show("Interview marked completed and booth visit updated.");
                    else
                        MessageBox.Show("Update failed.");
                }
            }

            // Optional: Refresh the DataGridView
            LoadAvailableScheduledInterviewsForStudent();
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReviewStd reviewStd = new ReviewStd();
            reviewStd.Show();
        }
    }
}
/*  scheduleinterview outdatd button
 if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an interview from the table.");
                return;
            }

            // Get selected interviewId
            int interviewId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SelectedInterviewId"].Value);

            string connectionString = "Data Source=ALMIGHTYPUSH\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string updateQuery = "UPDATE INTERVIEW SET interviewStatus = 'Scheduled' WHERE interviewId = @interviewId";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@interviewId", interviewId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Interview status updated to Scheduled.");
            LoadInterviewData(); // Refresh the DataGridView
        }
*/
        
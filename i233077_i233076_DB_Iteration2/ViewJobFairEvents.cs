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
    public partial class ViewJobFairEvents : Form
    {
        public ViewJobFairEvents()
        {
            InitializeComponent();
            LoadJobFairEvents();
        }

        public List<JobFairEvent> GetJobFairEvents()
        {
            List<JobFairEvent> events = new List<JobFairEvent>();

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM JOBFAIREVENT";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JobFairEvent evt = new JobFairEvent
                            {
                                JobFairEventId = reader.GetInt32(reader.GetOrdinal("jobFairEventId")),
                                EventName = reader.GetString(reader.GetOrdinal("eventName")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                Status = reader.GetString(reader.GetOrdinal("status")),
                                EventDate = reader.GetDateTime(reader.GetOrdinal("eventDate")),
                                EventTime = reader.GetTimeSpan(reader.GetOrdinal("eventTime")),
                                Location = reader.GetString(reader.GetOrdinal("location")),
                                AdminId = reader.GetInt32(reader.GetOrdinal("adminId"))
                            };

                            events.Add(evt);
                        }
                    }
                }
            }

            return events;
        }
        private void LoadJobFairEvents()
        {
            List<JobFairEvent> events = GetJobFairEvents();
            dataGridView1.DataSource = events;
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

        private void button4_Click(object sender, EventArgs e)
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

        private void Review_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReviewStd form = new ReviewStd();
            form.Show();
        }
    }
}

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
    public partial class ScheduleInterview : Form
    {
        public ScheduleInterview()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string interviewDate = textBox1.Text;
            string interviewTime = textBox3.Text;
            string boothId = textBox2.Text;
            int? applicationId = null;
            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO INTERVIEW (interviewDate, interviewTime, interviewStatus, boothId, applicationId, recruiterId)
                         VALUES (@date, @time, 'Scheduled', @boothId, @applicationId, @recruiterId)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@date", DateTime.Parse(interviewDate));
                cmd.Parameters.AddWithValue("@time", TimeSpan.Parse(interviewTime));
                cmd.Parameters.AddWithValue("@boothId", int.Parse(boothId));
                cmd.Parameters.AddWithValue("@applicationId", (object)applicationId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@recruiterId", RecruiterData.SelectedRecruiterID);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Interview Scheduled Successfully!");
                    this.Hide();
                    RecruiterDashBoard dashboard = new RecruiterDashBoard();
                    dashboard.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            RecruiterDashBoard dashboard = new RecruiterDashBoard();
            dashboard.Show();
        }
    }
}

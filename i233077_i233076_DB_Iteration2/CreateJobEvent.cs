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
    public partial class CreateJobEvent : Form
    {
        public CreateJobEvent()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard dashboard =  new AdminDashboard();
            dashboard.Show();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            string eventName = nametextBox.Text;
            string description = descriptiontextBox.Text;
            string status = statuscomboBox.Text;
            DateTime selectedDate = datePicker.Value.Date;         // Only the date part
            TimeSpan eventTime = TimePicker.Value.TimeOfDay;       // Only the time part
            string location = locationtextBox.Text;
            if (string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(status) || string.IsNullOrEmpty(location) || selectedDate == null)
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            if (selectedDate.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Event date cannot be in the past.");
                return;
            }

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string insertQuery = "INSERT INTO JOBFAIREVENT (eventName, description, status, eventDate, eventTime, location, adminId) VALUES (@eventName, @description, @status, @eventDate, @eventTime, @location, @adminId)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@eventName", eventName);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@eventDate", selectedDate);
                    cmd.Parameters.AddWithValue("@eventTime", eventTime);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@adminId", UserData.SelectedUserId); // Replace this with actual adminId variable

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Event saved successfully.");
                        this.Hide();
                        AdminDashboard adminDashboard = new AdminDashboard();
                        adminDashboard.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving event: " + ex.Message);
                    }
                }
            }
        }
    }
}

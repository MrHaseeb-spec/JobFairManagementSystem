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
    public partial class DataFormBoothCoordinator : Form
    {
        public DataFormBoothCoordinator()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            TimeSpan shiftStartTime = StartTimedateTimePicker.Value.TimeOfDay;
            TimeSpan shiftEndTime = EndTimedateTimePicker.Value.TimeOfDay;
            if (shiftStartTime== null || shiftEndTime == null)
            {
                MessageBox.Show("Please select both shift start and end times.");
                return;
            }
            if (shiftStartTime >= shiftEndTime)
            {
                MessageBox.Show("Shift start time must be earlier than the shift end time.");
                return;
            }


            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string insertQuery = "INSERT INTO BOOTHCOORDINATOR (shiftStartTime, shiftEndTime, userId) VALUES (@shiftStartTime, @shiftEndTime, @userId)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
            insertCmd.Parameters.AddWithValue("@shiftStartTime", shiftStartTime);
            insertCmd.Parameters.AddWithValue("@shiftEndTime", shiftEndTime);
            insertCmd.Parameters.AddWithValue("@userId", UserData.SelectedUserId);
            insertCmd.ExecuteNonQuery();
            BoothCoordinatorData.ShiftStartTime = shiftStartTime;
            BoothCoordinatorData.ShiftEndTime = shiftEndTime;
            MessageBox.Show("Data submitted successfully!");
            this.Hide();
            BoothCoordinatorDashboard loginForm = new BoothCoordinatorDashboard();
            loginForm.Show();
        }
    }
}

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
    public partial class BoothCoordinatorDashboard : Form
    {
        public BoothCoordinatorDashboard()
        {
            InitializeComponent();
            showInfo();
        }

        private void BoothCoordinatorDashboard_Load(object sender, EventArgs e)
        {
            textBox1.Text = BoothCoordinatorData.SelectedCoordinatorId.ToString();
            textBox2.Text = UserData.SelectedName;
            textBox3.Text = BoothCoordinatorData.ShiftStartTime.ToString();
            textBox4.Text = UserData.SelectedEmail;
            textBox5.Text = BoothCoordinatorData.ShiftEndTime.ToString();
            textBox6.Text = UserData.SelectedPhoneNumber;
            textBox9.Text = BoothCoordinatorData.boothId.ToString();
        }

        private void showInfo()
        {
            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";
            string query = "SELECT * FROM BOOTHCOORDINATOR WHERE userId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", UserData.SelectedUserId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            BoothCoordinatorData.SelectedCoordinatorId = reader.GetInt32(reader.GetOrdinal("boothCoordinatorId"));
                            BoothCoordinatorData.ShiftStartTime = reader.GetTimeSpan(reader.GetOrdinal("shiftStartTime"));
                            BoothCoordinatorData.ShiftEndTime = reader.GetTimeSpan(reader.GetOrdinal("shiftEndTime"));
                        }
                    }
                }
                string query2 = "SELECT boothId FROM BOOTH WHERE boothCoordinatorId = @boothCoordinatorId";
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@boothCoordinatorId", BoothCoordinatorData.SelectedCoordinatorId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            BoothCoordinatorData.boothId = reader.GetInt32(reader.GetOrdinal("boothId"));
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            BoothReport boothCoordinatorReport = new BoothReport();
            boothCoordinatorReport.Show();
        }
    }
}

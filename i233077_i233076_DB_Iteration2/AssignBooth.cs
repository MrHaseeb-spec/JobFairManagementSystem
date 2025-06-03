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
    public partial class AssignBooth : Form
    {
        private string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

        public AssignBooth()
        {
            InitializeComponent();
            LoadCompanyIDs();
            LoadBoothCoordinatorIDs();
        }

        private void LoadCompanyIDs()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT companyId, name FROM COMPANY";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No companies available. Returning to dashboard.");
                    this.Hide();
                    AdminDashboard dashboard = new AdminDashboard();
                    dashboard.Show();
                    return;
                }

                companycomboBox.DisplayMember = "name";
                companycomboBox.ValueMember = "companyId";
                companycomboBox.DataSource = dt;
            }
        }

        private void LoadBoothCoordinatorIDs()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT boothCoordinatorId FROM BOOTHCOORDINATOR";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No booth coordinators available. Returning to dashboard.");
                    this.Hide();
                    AdminDashboard dashboard = new AdminDashboard();
                    dashboard.Show();
                    return;
                }

                coordinatorcomboBox.DisplayMember = "boothCoordinatorId";
                coordinatorcomboBox.ValueMember = "boothCoordinatorId";
                coordinatorcomboBox.DataSource = dt;
            }
            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard dashboard = new AdminDashboard();
            dashboard.Show();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            string location = locationtextBox.Text;
            int companyId = Convert.ToInt32(companycomboBox.SelectedValue);
            int boothCoordinatorId = Convert.ToInt32(coordinatorcomboBox.SelectedValue);
            int adminId = UserData.SelectedUserId;

            if (string.IsNullOrEmpty(location))
            {
                MessageBox.Show("Please enter a location.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO BOOTH (location, adminId, companyId, boothCoordinatorId) VALUES (@location, @adminId, @companyId, @boothCoordinatorId)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@adminId", adminId);
                    cmd.Parameters.AddWithValue("@companyId", companyId);
                    cmd.Parameters.AddWithValue("@boothCoordinatorId", boothCoordinatorId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Booth assigned successfully!");
                    this.Hide();
                    AdminDashboard dashboard= new AdminDashboard();
                    dashboard.Show();
                }
            }
        }

    }
}

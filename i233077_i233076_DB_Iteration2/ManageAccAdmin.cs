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
    public partial class ManageAccAdmin : Form
    {
        // Replace with your actual SQL Server connection string
        //Saad
        //SqlConnection conn = new SqlConnection(" Data source = ALMIGHTYPUSH\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        //Haseeb
        string connectionString = "Data Source = DESKTOP - JUIJCBN\\SQLEXPRESS; Initial Catalog = JOBFAIR; Integrated Security = True; Encrypt = False";

        public ManageAccAdmin()
        {
            InitializeComponent();
            ReadUsers();
        }

        private void ReadUsers()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("userId");
            dataTable.Columns.Add("name");
            dataTable.Columns.Add("email");
            dataTable.Columns.Add("password");
            dataTable.Columns.Add("phone number");
            dataTable.Columns.Add("role");
            dataTable.Columns.Add("isActive");
            dataTable.Columns.Add("isApproved");
            var repo = new UserRepository();
            var users = repo.getNotApproved();
            foreach(var user in users)
            {
                var row = dataTable.NewRow();
                row["userId"] = user.SelectedUserId;
                row["name"] = user.SelectedName;
                row["email"] = user.SelectedEmail;
                row["password"] = user.SelectedPassword;
                row["phone number"] = user.SelectedPhoneNumber;
                row["role"] = user.SelectedRole;
                row["isActive"] = user.SelectedActive;
                row["isApproved"] = user.SelectedApproval;

                dataTable.Rows.Add(row);
            }
            this.dataGridView1.DataSource = dataTable;
        }


        private void Accept_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("No row selected.");
                return;
            }

            int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["userId"].Value);
            bool isActive = true;
            bool isApproved = true;
            var repo = new UserRepository();
            repo.updateUserStatus(userId, isActive, isApproved);
            ReadUsers();
        }



        // Other navigation buttons
        private void dashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard dashboard = new AdminDashboard();
            dashboard.Show();
        }

        private void Activate_Deactivate_Click(object sender, EventArgs e)
        {
            this.Hide();
            Activate_deactivate dashboard = new Activate_deactivate();
            dashboard.Show();
        }

        private void JobFairEvents_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateJobEvent dashboard = new CreateJobEvent();
            dashboard.Show();
        }

        private void AssignBooths_Click(object sender, EventArgs e)
        {
            this.Hide();
            AssignBooth dashboard = new AssignBooth();
            dashboard.Show();
        }

        private void Student_Reports_Click(object sender, EventArgs e)
        {
            this.Hide();
            StdRepots dashboard = new StdRepots();
            dashboard.Show();
        }

        private void Recruiter_Reports_Click(object sender, EventArgs e)
        {
            this.Hide();
            RecruiterReports dashboard = new RecruiterReports();
            dashboard.Show();
        }

        private void Placement_Statistics_Click(object sender, EventArgs e)
        {
            this.Hide();
            placementStats dashboard = new placementStats();
            dashboard.Show();
        }

        private void Event_Performance_Click(object sender, EventArgs e)
        {
            this.Hide();
            EventPerformance dashboard = new EventPerformance();
            dashboard.Show();
        }

        private void Log_Out_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login dashboard = new Login();
            dashboard.Show();
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            IDtextBox.Text = UserData.SelectedUserId.ToString();
            NametextBox.Text = UserData.SelectedName;
            EmailtextBox.Text = UserData.SelectedEmail;
            PhonetextBox.Text = UserData.SelectedPhoneNumber;

        }

        private void Manage_Accounts_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageAccAdmin dashboard = new ManageAccAdmin();
            dashboard.Show();

        }

        private void Activate_deactivate_Click(object sender, EventArgs e)
        {
            this.Hide();
            Activate_deactivate dashboard = new Activate_deactivate();
            dashboard.Show();

        }

        private void JobEvents_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateJobEvent dashboard = new CreateJobEvent();
            dashboard.Show();
        }

        private void Assign_booth_Click(object sender, EventArgs e)
        {
            this.Hide();
            AssignBooth dashboard = new AssignBooth();
            dashboard.Show();
        }

        private void Std_reports_Click(object sender, EventArgs e)
        {
            this.Hide();
            StdRepots dashboard = new StdRepots();
            dashboard.Show();
        }

        private void recruiter_rpt_Click(object sender, EventArgs e)
        {
            this.Hide();
            RecruiterReports dashboard = new RecruiterReports();
            dashboard.Show();
        }

        private void placement_stat_Click(object sender, EventArgs e)
        {
            this.Hide();
            placementStats dashboard = new placementStats();
            dashboard.Show();
        }

        private void event_perf_Click(object sender, EventArgs e)
        {
            this.Hide();
            EventPerformance dashboard = new EventPerformance();
            dashboard.Show();
        }

        private void log_out_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login dashboard = new Login();
            dashboard.Show();
        }
    }
}

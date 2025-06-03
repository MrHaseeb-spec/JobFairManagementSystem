using Microsoft.Reporting.WinForms;
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
    public partial class EventPerformance : Form
    {
        public EventPerformance()
        {
            InitializeComponent();
            reporting_1();
            reporting_2();
        }
        private void reporting_1()
        {
            var adapter = new BoothTrafficTableAdapters.BOOTHTrafficTableAdapter();
            var table = new BoothTraffic.BOOTHTrafficDataTable();
            adapter.Fill(table);

            ReportDataSource rds = new ReportDataSource("BoothTrafficDataSet", (DataTable)table);

            reportViewer1.LocalReport.ReportPath = "BoothTraffic.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void reporting_2()
        {
            var adapter = new ParticipationHourTableAdapters.ParticipationHourTableAdapter();
            var table = new ParticipationHour.ParticipationHourDataTable();
            adapter.Fill(table);

            ReportDataSource rds = new ReportDataSource("ParticipationHourDataSet", (DataTable)table);

            reportViewer2.LocalReport.ReportPath = "ParticipationHour.rdlc";
            reportViewer2.LocalReport.DataSources.Clear();
            reportViewer2.LocalReport.DataSources.Add(rds);
            reportViewer2.RefreshReport();

        }

        private void Dashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard dashboard = new AdminDashboard();
            dashboard.Show();

        }

        private void Manage_Accounts_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageAccAdmin dashboard = new ManageAccAdmin();
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

        private void Assign_Booths_Click(object sender, EventArgs e)
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

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
    public partial class placementStats : Form
    {
        public placementStats()
        {
            InitializeComponent();
            reporting_1();
            reporting_2();
            reporting_3();
        }

        private void reporting_1()
        {
            var adapter = new OverallPercentageTableAdapters.OverallPercentageTableAdapter();
            var table = new OverallPercentage.OverallPercentageDataTable();
            adapter.Fill(table);

            ReportDataSource rds = new ReportDataSource("OverallPercentageDataSet", (DataTable)table);

            reportViewer1.LocalReport.ReportPath = "OverallPercentage.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void reporting_2()
        {
            var adapter = new PlacementPerDepartmentTableAdapters.PlacementPerDepartmentTableAdapter();
            var table = new PlacementPerDepartment.PlacementPerDepartmentDataTable();
            adapter.Fill(table);

            ReportDataSource rds = new ReportDataSource("PlacementPerDepartmentDataSet", (DataTable)table);

            reportViewer2.LocalReport.ReportPath = "PlacementPerDepartment.rdlc";
            reportViewer2.LocalReport.DataSources.Clear();
            reportViewer2.LocalReport.DataSources.Add(rds);
            reportViewer2.RefreshReport();
        }
        private void reporting_3()
        {
            var adapter = new AverageSalaryTableAdapters.AverageSalaryTableAdapter();
            var table = new AverageSalary.AverageSalaryDataTable();
            adapter.Fill(table);

            ReportDataSource rds = new ReportDataSource("AverageSalaryDataSet", (DataTable)table);

            reportViewer3.LocalReport.ReportPath = "AverageSalary.rdlc";
            reportViewer3.LocalReport.DataSources.Clear();
            reportViewer3.LocalReport.DataSources.Add(rds);
            reportViewer3.RefreshReport();
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

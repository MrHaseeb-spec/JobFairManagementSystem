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
    public partial class BoothReport : Form
    {
        public BoothReport()
        {
            InitializeComponent();
            reporting_1();
        }

        private void reporting_1()
        {
            var adapter = new BoothCoordinatorReportTableAdapters.VisitorsTableAdapter();
            var table = new BoothCoordinatorReport.VisitorsDataTable();
            adapter.Fill(table);

            ReportDataSource rds = new ReportDataSource("VisitorDataSet", (DataTable)table);

            reportViewer1.LocalReport.ReportPath = "BoothCoordinatorReport.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            BoothCoordinatorDashboard dashboard = new BoothCoordinatorDashboard();
            dashboard.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

        }
    }
}

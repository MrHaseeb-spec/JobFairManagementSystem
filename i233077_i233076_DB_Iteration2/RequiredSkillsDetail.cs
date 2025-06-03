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
    public partial class RequiredSkillsDetail : Form
    {
        public RequiredSkillsDetail()
        {
            InitializeComponent();
            LoadRequiredSkills();
        }
        private void LoadRequiredSkills()
        {
            string connectionString = "your_connection_string_here";
            string query = @"SELECT description 
                     FROM REQUIREDSKILLS 
                     WHERE jobPostingId = @JobId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@JobId", RequiredJobSkillsShow.jobId);

                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // Bind to DataGridView
                    dataGridView1.DataSource = table;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading required skills: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            JobViewstd form = new JobViewstd();
            form.Show();
        }
    }
}
